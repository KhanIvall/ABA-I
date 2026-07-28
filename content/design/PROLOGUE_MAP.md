# Prólogo — Northern Continent (mapa y nodos)

## Referencias visuales

| Archivo | Uso |
|---------|-----|
| [`northern_continent_outline.png`](northern_continent_outline.png) | Silueta tierra/agua (base) |
| [`northern_continent_points.png`](northern_continent_points.png) | Layout autor: regiones + Koro (esquema) |
| [`northern_continent_geo_matanui.png`](northern_continent_geo_matanui.png) | Mapa **solo geografía** (biomas procedurales limpios; sin textos) |
| [`northern_continent_poi_matanui.png`](northern_continent_poi_matanui.png) | Misma geografía + marcadores Koro + leyenda en franja inferior |

**Reglas de viaje (fijas):**
- El continente **no** es un sandbox ni una escena gigante continua: no se camina de un local a otro por el mapa.
- El **Mapa Global** es la **única** forma de ir entre Mapas Locales (fast travel por nodos).
- Cada **Mapa Local** es una escena separada (Koro, suva, guarida, POI, etc.).
- Las **zonas** son registro de lore geográfico (regiones del continente); **no** son destinos de viaje ni hubs.
- Nodos del Global: no todos visibles ni accesibles al inicio; se desbloquean con la historia / flags.

---

## Cómo rellenar esto (para el autor)

Hay **dos listas**:

1. **Zonas** — regiones del continente (Ta-Wahi, Aldari Coast, etc.). Lore / organización. No abren escena.
2. **Nodos locales** — destinos visitables en el Global (Koro, suva, guaridas, POIs…). Cada uno tiene (o tendrá) un Mapa Local.

Cada nodo local apunta a una zona con `id_zona`.

**Tipos de nodo sugeridos:** `koro` · `suva` · `guarida` · `poi`

**Posición en el outline:** lenguaje natural o porcentajes (ej. “sureste, sobre la península”, o `~75% X, ~70% Y` desde arriba-izquierda).

### Plantilla — zona

| id | nombre | también llamado | región en el outline | notas |
|----|--------|-----------------|----------------------|-------|
| `nc_...` | | | | |

### Plantilla — nodo local

| id | nombre | tipo | id_zona | visible_inicio | accesible_inicio | notas |
|----|--------|------|---------|----------------|------------------|-------|
| `nc_...` | | koro/suva/guarida/poi | `nc_...` | sí/no | sí/no | |

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

## Nodos locales (destinos del Global)

Cada fila es un pin del Mapa Global y corresponde a un Mapa Local (escena). Más adelante se añaden suva, guaridas, etc. en la misma tabla.

| id | nombre | tipo | id_zona | visible_inicio | accesible_inicio | notas |
|----|--------|------|---------|----------------|------------------|-------|
| nc_ta_koro | Ta-Koro | koro | nc_ta_wahi | sí | sí | En medio de las planicies |
| nc_ga_koro | Ga-Koro | koro | nc_ga_wahi | sí | sí | Gran asentamiento portuario en la costa |
| nc_le_koro | Le-Koro | koro | nc_le_wahi | sí | sí | A las afueras del bosque que rodea Aviro Glades |
| nc_po_koro | Po-Koro | koro | nc_po_wahi | no | no | Cañón en las Barrens; casas talladas en los muros |
| nc_ko_koro | Ko-Koro | koro | nc_ko_wahi | no | no | En lo alto del monte; igloo-like huts |
| nc_onu_koro | Onu-Koro | koro | nc_onu_wahi | no | no | Asentamiento minero a la entrada de The Mines, a los pies de Mount Rapovi |
| nc_de_koro | De-Koro | koro | nc_de_wahi | no | no | En un silencioso y rocoso acantilado |
| nc_ta_suva | Ta-Suva | suva | nc_ta_wahi | no | no | Shrine en Illidora Plains |

---

## Escenas locales previstas

Una fila por cada **nodo local** (no por zona). Anota la escena aunque el `.tscn` aún no exista.

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
