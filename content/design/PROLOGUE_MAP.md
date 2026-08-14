# Prólogo — Northern Continent (mapa y nodos)

## Referencias visuales

| Archivo | Uso |
|---------|-----|
| [`northern_continent_outline.png`](northern_continent_outline.png) | Silueta tierra/agua (base) |
| [`northern_continent_points.png`](northern_continent_points.png) | Layout autor: regiones + Koro (esquema) |
| [`northern_continent_geo_matanui.png`](northern_continent_geo_matanui.png) | Mapa **solo geografía** (biomas procedurales limpios; sin textos) |
| [`northern_continent_poi_matanui.png`](northern_continent_poi_matanui.png) | Misma geografía + marcadores Koro + leyenda en franja inferior |

**Diseño relacionado:**
- Misiones: [`PROLOGUE_QUESTS.md`](PROLOGUE_QUESTS.md)
- Personajes: [`PROLOGUE_CHARS.md`](PROLOGUE_CHARS.md)
- Ítems: [`PROLOGUE_ITEMS.md`](PROLOGUE_ITEMS.md)

**Reglas de viaje (fijas):**
- El **Mapa Global** es un overworld por **áreas/secciones**: dentro de un área el jugador se mueve con libertad.
- Las áreas se **conectan solo por nodos nexo** (tipo `pass`). Sin pasar por un nexo accesible no se cruza de un área a otra.
- Entrar a un **Mapa Local** (Koro, suva, glade, etc.) es salir del overworld a una **escena separada**; no hay un único sandbox continuo continente↔interiores.
- Las **zonas** (Ta-Wahi, etc.) son lore geográfico; **no** son el grafo de movilidad (ese son áreas + nodos).
- Nodos: no todos visibles/accesibles al inicio. Se abren por historia/flags y/o por **descubrimiento al acercarse** (`descubrimiento_pct`).
- Detalle de movilidad: sección [Movilidad en el Mapa Global](#movilidad-en-el-mapa-global).

---

## Cómo rellenar esto (para el autor)

Hay **tres listas**:

1. **Zonas** — lore geográfico (Ta-Wahi, Aldari Coast…). No definen movilidad.
2. **Áreas** — secciones del **Mapa Global** donde el jugador se mueve libremente. Se cruzan solo por nodos `pass`.
3. **Nodos** — destinos (Mapa Local) o nexos (`pass`) dentro / entre áreas.

Cada nodo apunta a una **zona** (`id_zona`, lore) y a un **área** (`id_area`, movilidad). Un `pass` en el borde puede listarse en el área “de salida” habitual; las conexiones entre áreas se anotan en la tabla de áreas.

**Tipos de nodo sugeridos:** `koro` · `suva` · `guarida` · `poi` · `pass` · `glade`

- `pass` = nexo entre áreas del Global.
- El resto suelen abrir un **Mapa Local** al interactuar / entrar.

**Posición en el outline:** lenguaje natural o porcentajes (ej. “sureste, sobre la península”, o `~75% X, ~70% Y` desde arriba-izquierda).

**`descubrimiento_pct`:** para nodos **ocultos** (`visible_inicio`/`accesible_inicio` = no) que pueden revelarse al **acercarse** el jugador en el Global. Valor `0`–`100` = % de probabilidad de pasar a visible+accesible en ese acercamiento. Vacío o `-` = no usa descubrimiento por proximidad (solo misión/flag, o ya visible al inicio).

### Plantilla — zona

| id | nombre | también llamado | región en el outline | notas |
|----|--------|-----------------|----------------------|-------|
| `nc_...` | | | | |

### Plantilla — área (Mapa Global)

| id | nombre | zonas_asociadas | descripción | nexos (`pass`) | notas |
|----|--------|-----------------|-------------|----------------|-------|
| `area_...` | | `nc_...` | | `nc_...` | |

### Plantilla — nodo

| id | nombre | tipo | id_zona | id_area | visible_inicio | accesible_inicio | descubrimiento_pct | notas |
|----|--------|------|---------|---------|----------------|------------------|--------------------|-------|
| `nc_...` | | koro/suva/pass/… | `nc_...` | `area_...` | sí/no | sí/no | `-` o 0–100 | |

---

## Zonas (lore geográfico)

Registro de las regiones que componen el continente. Los Mapas Locales viven *dentro* de estas zonas; la zona en sí no es un pin de viaje.

| id | nombre | también llamado | región en el outline | notas |
|----|--------|-----------------|----------------------|-------|
| nc_ta_wahi | Ta-Wahi | Illidora Plains | centro-este del cuerpo principal | Extensas planicies |
| nc_ga_wahi | Ga-Wahi | Aldari Coast | centro-sur del cuerpo principal | Costa altamente comercial |
| nc_le_wahi | Le-Wahi | Aviro Glades | sudoeste del cuerpo central | Claro floreado en medio de un bosque |
| nc_po_wahi | Po-Wahi | Northern Barrens | centro-norte del cuerpo central | Tierras baldías y áridas |
| nc_ko_wahi | Ko-Wahi | Mount Rapovi | centro, ligeramente al oeste | Alta montaña nevada |
| nc_onu_wahi | Onu-Wahi | The Mines | centro, ligeramente al este | Zona minera |
| nc_tr_kr_pen | Tren Krom Peninsula | | sudeste del continente | Zona accidentada: acantilados y peligros |
| nc_tiru_lake | Tiru Lake | | sudeste del continente, norte de la Peninsula | Lago que separa el continente de la península; de él sale un río al mar hacia el este |
| nc_de_wahi | De-Wahi | | extremo sur de la Peninsula | Zona baldía, poca vida silvestre, muy silenciosa |

---

## Áreas del Mapa Global

Secciones de overworld: movimiento libre **dentro** de cada área; cruce a otra área solo por un `pass` accesible. No confundir con **zonas** (lore).

| id | nombre | zonas_asociadas | descripción | nexos (`pass`) | notas |
|----|--------|-----------------|-------------|----------------|-------|
| area_ga_01 | Cercanías de Ga-Koro | nc_ga_wahi | Overworld alrededor del puerto y costa Aldari | nc_ga_le_korin, nc_collapse | Conectada con `area_le_01` por el río Korin y `area_road_01` por el paso colapsado |
| area_le_01 | Cercanías de Le-Koro | nc_le_wahi | Overworld alrededor de Le-Koro y Aviro Glades | nc_ga_le_korin | Conectada con `area_ga_01` por el río Korin |
| area_road_01 | Gran Camino | nc_ga_wahi, nc_onu_wahi, nc_ta_wahi | Camino que conecta Ga-Wahi, Onu-Wahi y Ta-Wahi | nc_collapse | Acceso ligado a `pq_003` / `f_vis_road` (ajustar cuando definas el resto) |
| area_aviro | Bosque Aviro | nc_le_wahi | Overworld del bosque de Aviro Glades | nc_le_koro | Conectada con `area_le_01` por Aviro Gates en Le-Koro |
| area_ta_01 | Cercanías de Ta-Koro | nc_ta_wahi | Overworld de Illidora Plains | nc_mountain_tunnel | Conectada con `area_road_01` por el tunel en la montaña |



---

## Nodos (destinos y nexos del Global)

Cada fila es un punto del Mapa Global: **destino** (Mapa Local) o **nexo** (`pass` entre áreas).

| id | nombre | tipo | id_zona | id_area | visible_inicio | accesible_inicio | descubrimiento_pct | notas |
|----|--------|------|---------|---------|----------------|------------------|--------------------|-------|
| nc_ta_koro | Ta-Koro | koro | nc_ta_wahi | area_ta_01 | sí | sí | - | En medio de las planicies |
| nc_ga_koro | Ga-Koro | koro | nc_ga_wahi | area_ga_01 | sí | sí | - | Gran asentamiento portuario en la costa. Escena inicial, Lhikan llega al puerto tras un viaje. |
| nc_le_koro | Le-Koro | koro / pass | nc_le_wahi | area_le_01 | sí | sí | - | A las afueras del bosque que rodea Aviro Glades. Nexo `area_le_01` ↔ `area_aviro`; a través de Aviro Gates |
| nc_po_koro | Po-Koro | koro | nc_po_wahi | | no | no | 100 | Cañón en las Barrens; casas talladas en los muros |
| nc_ko_koro | Ko-Koro | koro | nc_ko_wahi | | no | no | 100 | En lo alto del monte; igloo-like huts |
| nc_onu_koro | Onu-Koro | koro | nc_onu_wahi | | no | no | 100 | Asentamiento minero a la entrada de The Mines, a los pies de Mount Rapovi |
| nc_de_koro | De-Koro | koro | nc_de_wahi | | no | no | 100 | En un silencioso y rocoso acantilado |
| nc_ta_suva | Ta-Suva | suva | nc_ta_wahi | | no | no | 100 | Shrine en Illidora Plains |
| nc_ga_suva | Ga-Suva | suva | nc_ga_wahi | area_ga_01 | no | no | 100 | Shrine en Aldari Coast |
| nc_ga_le_korin | Korin River Pass | pass | nc_ga_wahi | area_ga_01 | sí | sí | - | Nexo `area_ga_01` ↔ `area_le_01`; puente roto |
| nc_collapse | Collapsed Pass | pass | nc_ga_wahi | area_ga_01 | sí | no | - | Nexo `area_ga_01` ↔ `area_road_01`; derrumbe. Accesible tras `pq_003` |
| nc_glade | The Glade | glade | nc_le_wahi | area_aviro | sí | sí | - | Claro rodeado de arboles y cubierto de flores de diversos colores |
| nc_mountain_tunnel | The Mountain Tunnel | pass | nc_onu_wahi | area_road_01 | sí | sí | - | Nexo `area_road_01` ↔ `area_ta_01`; tunel |
| nc_rock_wall | Rock Wall | poi | nc_onu_wahi | area_road_01 | no | no | 100 | Espacio rocoso al rededor de una muralla rocosa, que es el pie de una montaña. Ahí se encuentra el cadaver de un guardia Ta-Matoran |

---

## Escenas locales previstas

Una fila por cada **nodo** (no por zona). Anota la escena aunque el `.tscn` aún no exista. Los `pass` pueden tener escena propia (cruce del río, etc.) o resolverse solo en el Global; anótalo en la frase.

| id_nodo | id_escena_local (previsto) | qué es en una frase |
|---------|----------------------------|---------------------|
| nc_ta_koro | local_ta_koro | Aldea de fuego en Ta-Wahi |
| nc_ga_koro | local_ga_koro | Aldea de agua en Ga-Wahi |
| nc_le_koro | local_le_koro | Aldea de aire en Le-Wahi |
| nc_po_koro | local_po_koro | Aldea de piedra en Po-Wahi |
| nc_ko_koro | local_ko_koro | Aldea de hielo en Ko-Wahi |
| nc_onu_koro | local_onu_koro | Aldea de tierra en Onu-Wahi |
| nc_de_koro | local_de_koro | Aldea de sonido en De-Wahi |
| nc_ta_suva | local_ta_suva | Suva de fuego en Ta-Wahi |
| nc_ga_suva | local_ga_suva | Suva de agua en Ga-Wahi |
| nc_ga_le_korin | local_ga_le_korin | Paso del Río Korin (nexo Ga↔Le) |
| nc_collapse | local_collapse | Paso colapsado (nexo Ga↔Camino) |
| nc_glade | local_glade | Claro de Le-Wahi |
| nc_mountain_tunnel | local_mountain_tunnel | Tunel que atravieza la montaña |

---

## Movilidad en el Mapa Global

El Mapa Global funciona por **áreas** (tabla [Áreas del Mapa Global](#áreas-del-mapa-global)) interconectadas por nodos nexo.

- **Dentro de un área:** movimiento libre; acercarse a nodos de esa sección (descubrimiento, entrar a destinos, etc.).
- **Entre áreas:** solo por un nodo tipo **`pass`** accesible (ej. `nc_ga_le_korin`: `area_ga_01` ↔ `area_le_01`).
- Un `pass` puede estar visible pero **no accesible** hasta una misión/flag (ej. `nc_collapse` tras `pq_003`: `area_ga_01` ↔ `area_road_01`).
- Esto **no** es fast travel de “elegir cualquier pin y teletransportarse”: overworld + gates entre secciones.
- Los **Mapas Locales** son escenas aparte al entrar a un destino (aldea, suva, claro…).
