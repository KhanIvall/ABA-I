"""
Build Northern Continent maps from northern_continent_outline.png only.

No AI concept plate — biomes are painted procedurally so the result stays clean
(no baked labels, UI panels, or white lake blobs).
"""
from __future__ import annotations

from pathlib import Path

import numpy as np
from PIL import Image, ImageDraw, ImageEnhance, ImageFilter, ImageFont

ROOT = Path(__file__).resolve().parents[1]
DESIGN = ROOT / "content" / "design"
OUTLINE = DESIGN / "northern_continent_outline.png"
OUT_GEO = DESIGN / "northern_continent_geo_matanui.png"
OUT_POI = DESIGN / "northern_continent_poi_matanui.png"

# Region paints: (name, fx, fy, RGB). Fractions of land bbox.
BIOMES = [
    ("barrens", 0.48, 0.12, (168, 132, 88)),
    ("plains", 0.78, 0.30, (210, 110, 55)),
    ("mount", 0.30, 0.40, (210, 220, 230)),
    ("mines", 0.55, 0.46, (70, 58, 52)),
    ("glades", 0.18, 0.70, (55, 130, 60)),
    ("coast", 0.48, 0.82, (186, 168, 120)),
    ("peninsula", 0.88, 0.90, (150, 118, 72)),
]

KORO = [
    ("Ta-Koro", (0.78, 0.32), (210, 55, 45)),
    ("Ga-Koro", (0.48, 0.78), (45, 110, 210)),
    ("Le-Koro", (0.20, 0.66), (50, 160, 60)),
    ("Po-Koro", (0.48, 0.14), (150, 100, 55)),
    ("Ko-Koro", (0.32, 0.42), (235, 240, 250)),
    ("Onu-Koro", (0.55, 0.45), (120, 70, 170)),
    ("De-Koro", (0.88, 0.92), (230, 200, 50)),
]


def dilate(mask: np.ndarray, radius: int = 2) -> np.ndarray:
    img = Image.fromarray((mask.astype(np.uint8) * 255))
    for _ in range(radius):
        img = img.filter(ImageFilter.MaxFilter(3))
    return np.array(img) > 127


def dilate_u8(mask: np.ndarray, radius: int = 2) -> Image.Image:
    """Dilate keeping a writable L image (needed for reliable Pillow floodfill)."""
    img = Image.fromarray((mask.astype(np.uint8) * 255), mode="L")
    for _ in range(radius):
        img = img.filter(ImageFilter.MaxFilter(3))
    return img


def erode(mask: np.ndarray, radius: int = 1) -> np.ndarray:
    img = Image.fromarray((mask.astype(np.uint8) * 255))
    for _ in range(radius):
        img = img.filter(ImageFilter.MinFilter(3))
    return np.array(img) > 127


def soft_mask(mask: np.ndarray, blur: float = 1.2) -> np.ndarray:
    img = Image.fromarray((mask.astype(np.uint8) * 255)).filter(ImageFilter.GaussianBlur(blur))
    return np.array(img).astype(np.float32) / 255.0


def build_masks(outline: Image.Image) -> tuple[np.ndarray, np.ndarray, np.ndarray]:
    rgb = np.array(outline.convert("RGB"))
    bri = rgb.mean(axis=2)
    # Core stroke only (ignore soft cyan glow) so floodfill can reach the ocean.
    white = bri > 190
    canvas = dilate_u8(white, 2).copy()
    for seed in (
        (0, 0),
        (canvas.size[0] - 1, 0),
        (0, canvas.size[1] - 1),
        (canvas.size[0] - 1, canvas.size[1] - 1),
    ):
        ImageDraw.floodfill(canvas, seed, 128, thresh=0)

    filled = np.array(canvas)
    if not np.any(filled == 128):
        raise RuntimeError("Ocean floodfill failed — outline stroke may touch the image border.")

    interior = filled == 0
    stroke = filled == 255

    # Tiru Lake = thick interior blob only (never peninsula stroke).
    eroded = Image.fromarray(stroke.astype(np.uint8) * 255).filter(ImageFilter.MinFilter(15))
    lake_core = np.array(eroded) > 127
    lake = np.zeros_like(lake_core)
    ys_c, xs_c = np.where(lake_core)
    if len(xs_c):
        cx, cy = int(xs_c.mean()), int(ys_c.mean())
        y0, y1 = max(cy - 50, 0), min(cy + 55, lake.shape[0])
        x0, x1 = max(cx - 65, 0), min(cx + 65, lake.shape[1])
        window = np.zeros_like(lake_core)
        window[y0:y1, x0:x1] = True
        lake = dilate(lake_core, 4) & window
        lake |= stroke & window & dilate(lake_core, 7)
        lake = dilate(erode(lake, 2), 3) & window

    land = (interior | (stroke & ~lake)) & ~lake
    land = dilate(land, 1) & ~lake
    ocean = ~(land | lake)
    return land, lake, ocean


def aldari_y_from_outline(outline_rgb: np.ndarray, xa: int, xb: int) -> int:
    bri = outline_rgb.mean(axis=2)
    core = bri > 200
    bottoms: list[int] = []
    for x in range(xa, xb):
        col = np.where(core[:, x])[0]
        if len(col):
            bottoms.append(int(col.max()))
    if not bottoms:
        soft = bri > 170
        for x in range(xa, xb):
            col = np.where(soft[:, x])[0]
            if len(col):
                bottoms.append(int(col.max()))
    return int(np.median(np.asarray(bottoms, dtype=np.int32)))


def straighten_aldari_south_coast(
    land: np.ndarray,
    lake: np.ndarray,
    outline_rgb: np.ndarray,
) -> np.ndarray:
    land = land.copy()
    h, _w = land.shape
    ys, xs = np.where(land & ~lake)
    x0, x1 = int(xs.min()), int(xs.max())
    xa = x0 + int(0.02 * (x1 - x0))
    xb = x0 + int(0.60 * (x1 - x0))
    y_coast = aldari_y_from_outline(outline_rgb, xa, xb)
    y_coast = int(np.clip(y_coast, int(h * 0.50), int(h * 0.78)))
    print(f"Aldari forced y_coast={y_coast} (xa={xa}, xb={xb})")

    for x in range(xa, xb):
        if not np.any(land[120:y_coast, x] & ~lake[120:y_coast, x]):
            continue
        land[y_coast + 1 :, x] = False
        land[y_coast - 55 : y_coast + 1, x] = True
        lake[y_coast - 10 : y_coast + 1, x] = False
        land[y_coast + 1 :, x] = False

    row = land[y_coast, xa:xb].copy()
    for i in range(1, len(row) - 1):
        if row[i - 1] and row[i + 1]:
            row[i] = True
    land[y_coast, xa:xb] = row
    for x in range(xa, xb):
        if land[y_coast, x]:
            land[y_coast + 1 :, x] = False
    return land


def land_bbox(land: np.ndarray) -> tuple[int, int, int, int]:
    ys, xs = np.where(land)
    return int(xs.min()), int(ys.min()), int(xs.max()), int(ys.max())


def frac_to_xy(land: np.ndarray, fx: float, fy: float) -> tuple[int, int]:
    x0, y0, x1, y1 = land_bbox(land)
    x = int(x0 + fx * (x1 - x0))
    y = int(y0 + fy * (y1 - y0))
    if 0 <= y < land.shape[0] and 0 <= x < land.shape[1] and land[y, x]:
        return x, y
    h, w = land.shape
    for r in range(1, 80):
        for dy in range(-r, r + 1):
            for dx in range(-r, r + 1):
                nx, ny = x + dx, y + dy
                if 0 <= nx < w and 0 <= ny < h and land[ny, nx]:
                    return nx, ny
    return x, y


def value_noise(h: int, w: int, scale: float, rng: np.random.Generator) -> np.ndarray:
    """Cheap smooth noise via upsampled random grid."""
    gh = max(int(h / scale) + 2, 2)
    gw = max(int(w / scale) + 2, 2)
    grid = rng.random((gh, gw)).astype(np.float32)
    img = Image.fromarray((grid * 255).astype(np.uint8), mode="L")
    img = img.resize((w, h), Image.Resampling.BICUBIC)
    return np.array(img).astype(np.float32) / 255.0


def paint_biomes(land: np.ndarray, rng: np.random.Generator) -> np.ndarray:
    """Soft distance blend of regional colors + multi-scale noise texture."""
    h, w = land.shape
    x0, y0, x1, y1 = land_bbox(land)
    yy, xx = np.mgrid[0:h, 0:w].astype(np.float32)

    centers = []
    colors = []
    for _name, fx, fy, rgb in BIOMES:
        cx = x0 + fx * (x1 - x0)
        cy = y0 + fy * (y1 - y0)
        centers.append((cx, cy))
        colors.append(np.array(rgb, dtype=np.float32))

    # Inverse-distance weights (soft Voronoi).
    weights = []
    for cx, cy in centers:
        d2 = (xx - cx) ** 2 + (yy - cy) ** 2 + 1.0
        # Stretch north-south slightly so coast band stays coastal.
        weights.append(1.0 / (d2 ** 1.15))
    wstack = np.stack(weights, axis=-1)
    wstack /= wstack.sum(axis=-1, keepdims=True)

    base = np.zeros((h, w, 3), dtype=np.float32)
    for i, col in enumerate(colors):
        base += wstack[..., i : i + 1] * col

    # Mount Rapovi: lift luminance near snow center.
    mx, my = centers[2]
    snow = np.exp(-(((xx - mx) ** 2 + (yy - my) ** 2) / (2 * (55.0**2))))
    snow_col = np.array([235, 240, 248], dtype=np.float32)
    base = base * (1.0 - snow[..., None] * 0.55) + snow_col * (snow[..., None] * 0.55)

    # Ta-Wahi heat: push orange near plains center.
    px, py = centers[1]
    heat = np.exp(-(((xx - px) ** 2 + (yy - py) ** 2) / (2 * (70.0**2))))
    heat_col = np.array([230, 90, 40], dtype=np.float32)
    base = base * (1.0 - heat[..., None] * 0.35) + heat_col * (heat[..., None] * 0.35)

    # Texture layers (painterly, not UI).
    n1 = value_noise(h, w, 28, rng)
    n2 = value_noise(h, w, 12, rng)
    n3 = value_noise(h, w, 5, rng)
    grain = (n1 - 0.5) * 28 + (n2 - 0.5) * 16 + (n3 - 0.5) * 8
    # Slight green flecks in glades / darker flecks in mines.
    gx, gy = centers[4]
    glade = np.exp(-(((xx - gx) ** 2 + (yy - gy) ** 2) / (2 * (80.0**2))))
    base[..., 1] += glade * 18
    nx, ny = centers[3]
    mine = np.exp(-(((xx - nx) ** 2 + (yy - ny) ** 2) / (2 * (60.0**2))))
    base -= mine[..., None] * 22

    out = base + grain[..., None]
    # Keep land-only; ocean filled later.
    return np.clip(out, 0, 255)


def ocean_texture(h: int, w: int, rng: np.random.Generator) -> np.ndarray:
    base = np.zeros((h, w, 3), dtype=np.float32)
    base[..., 0] = 12
    base[..., 1] = 38
    base[..., 2] = 68
    n = value_noise(h, w, 40, rng)
    n2 = value_noise(h, w, 18, rng)
    yy, xx = np.mgrid[0:h, 0:w]
    wave = (np.sin(xx / 41.0) * np.cos(yy / 57.0)) * 5.0
    out = base + ((n - 0.5) * 14 + (n2 - 0.5) * 8 + wave)[..., None]
    return np.clip(out, 0, 255)


def lake_texture(ocean: np.ndarray, rng: np.random.Generator) -> np.ndarray:
    out = ocean.copy()
    out[..., 0] = np.clip(out[..., 0] + 22, 0, 255)
    out[..., 1] = np.clip(out[..., 1] + 48, 0, 255)
    out[..., 2] = np.clip(out[..., 2] + 38, 0, 255)
    out += rng.normal(0, 2.5, out.shape)
    return np.clip(out, 0, 255)


def paint_south_beach(
    out: np.ndarray,
    land: np.ndarray,
    lake: np.ndarray,
    rng: np.random.Generator,
    depth: int = 8,
) -> np.ndarray:
    ys, xs = np.where(land & ~lake)
    x0, x1 = int(xs.min()), int(xs.max())
    xa = x0 + int(0.02 * (x1 - x0))
    xb = x0 + int(0.60 * (x1 - x0))
    out = out.copy()
    sand = np.array([200, 180, 135], dtype=np.float32)
    for x in range(xa, xb):
        col = np.where(land[:, x] & ~lake[:, x])[0]
        if not len(col):
            continue
        yb = int(col.max())
        for i, y in enumerate(range(yb, max(yb - depth, 0) - 1, -1)):
            if not land[y, x] or lake[y, x]:
                continue
            t = i / max(depth - 1, 1)
            base = out[y, x].astype(np.float32)
            colr = sand * (1.0 - t * 0.7) + base * (t * 0.7) + rng.normal(0, 2.5, 3)
            out[y, x] = np.clip(colr, 0, 255)
    return out


def paint_map(outline: Image.Image) -> tuple[Image.Image, np.ndarray, np.ndarray]:
    land, lake, _ocean = build_masks(outline)
    outline_rgb = np.array(outline.convert("RGB"))
    land = straighten_aldari_south_coast(land, lake, outline_rgb)

    h, w = land.shape
    rng = np.random.default_rng(7)
    terrain = paint_biomes(land, rng)
    ocean = ocean_texture(h, w, rng)
    lake_arr = lake_texture(ocean, rng)

    # Soft organic lake.
    lake = dilate(erode(lake, 1), 2)
    land = land & ~lake

    land_a = soft_mask(land, 0.55)[..., None]
    lake_a = soft_mask(lake, 2.8)[..., None]
    out = ocean * (1.0 - land_a) + terrain * land_a
    out = out * (1.0 - lake_a) + lake_arr * lake_a
    out = paint_south_beach(out, land, lake, rng)

    # Soft shore foam (subtle).
    edge = dilate(land, 1) & ~land & ~lake
    foam = soft_mask(edge, 1.0)[..., None]
    foam_col = np.array([140, 185, 205], dtype=np.float32)
    out = out * (1.0 - foam * 0.28) + foam_col * (foam * 0.28)

    lake_edge = dilate(lake, 1) & ~lake
    le = soft_mask(lake_edge, 1.2)[..., None]
    out = out * (1.0 - le * 0.22) + foam_col * (le * 0.22)

    img = Image.fromarray(np.clip(out, 0, 255).astype(np.uint8))
    img = ImageEnhance.Color(img).enhance(1.08)
    img = ImageEnhance.Contrast(img).enhance(1.04)
    return img, land, lake


def try_font(size: int) -> ImageFont.ImageFont:
    for name in ("segoeui.ttf", "arial.ttf", "calibri.ttf"):
        path = Path("C:/Windows/Fonts") / name
        if path.exists():
            return ImageFont.truetype(str(path), size)
    return ImageFont.load_default()


def _text_size(draw: ImageDraw.ImageDraw, text: str, font: ImageFont.ImageFont) -> tuple[int, int]:
    box = draw.textbbox((0, 0), text, font=font)
    return box[2] - box[0], box[3] - box[1]


def draw_poi(img: Image.Image, land: np.ndarray) -> Image.Image:
    footer_h = 72
    canvas = Image.new("RGB", (img.width, img.height + footer_h), (10, 28, 48))
    canvas.paste(img, (0, 0))
    draw = ImageDraw.Draw(canvas, "RGBA")
    for i in range(14):
        a = int(150 * (i / 13))
        draw.line([(0, img.height - 14 + i), (img.width, img.height - 14 + i)], fill=(10, 28, 48, a))

    draw = ImageDraw.Draw(canvas)
    font = try_font(13)
    font_lg = try_font(15)

    for _name, (fx, fy), color in KORO:
        x, y = frac_to_xy(land, fx, fy)
        r = 8
        draw.ellipse((x - r - 1, y - r - 1, x + r + 1, y + r + 1), fill=(0, 0, 0))
        draw.ellipse((x - r, y - r, x + r, y + r), fill=color, outline=(20, 20, 20), width=1)

    draw.text((12, img.height + 8), "Settlements", font=font_lg, fill=(200, 215, 230))
    for row_i, row in enumerate((KORO[:4], KORO[4:])):
        x = 12
        y = img.height + 32 + row_i * 20
        for name, _, color in row:
            draw.ellipse((x, y + 2, x + 11, y + 13), fill=color, outline=(0, 0, 0))
            draw.text((x + 16, y), name, font=font, fill=(220, 228, 235))
            tw, _ = _text_size(draw, name, font)
            x += 16 + tw + 22
    return canvas


def main() -> None:
    outline = Image.open(OUTLINE)
    base, land, lake = paint_map(outline)

    x0, _, x1, _ = land_bbox(land)
    mid0 = x0 + int(0.15 * (x1 - x0))
    mid1 = x0 + int(0.55 * (x1 - x0))
    bottoms = [
        int(np.where(land[:, x] & ~lake[:, x])[0].max())
        for x in range(mid0, mid1, 3)
        if np.any(land[:, x] & ~lake[:, x])
    ]
    if bottoms:
        print(
            f"Aldari south-edge y min/max/spread: {min(bottoms)}/{max(bottoms)}/{max(bottoms) - min(bottoms)}"
        )

    base.save(OUT_GEO)
    print(f"Wrote {OUT_GEO}")

    poi = draw_poi(base.copy(), land)
    poi.save(OUT_POI)
    print(f"Wrote {OUT_POI}")

    geo = np.array(base)
    print(f"Hot-white pixels (>235): {(geo.mean(axis=2) > 235).sum()}")
    print(f"Lake bbox pixels: {int(lake.sum())}")


if __name__ == "__main__":
    main()
