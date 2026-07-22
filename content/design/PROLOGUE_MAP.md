# Prólogo — Northern Continent (mapa y nodos)

## Referencias visuales

| Archivo | Uso |
|---------|-----|
| [`northern_continent_outline.png`](northern_continent_outline.png) | Silueta tierra/agua (base) |
| [`northern_continent_points.png`](northern_continent_points.png) | Layout autor: regiones + Koro (esquema) |
| [`northern_continent_geo_matanui.png`](northern_continent_geo_matanui.png) | Mapa **solo geografía** (biomas procedurales limpios; sin textos) |
| [`northern_continent_poi_matanui.png`](northern_continent_poi_matanui.png) | Misma geografía + marcadores Koro + leyenda en franja inferior |

**Reglas de viaje (fijas):**
- El **Mapa Global** es la **única** forma de ir entre áreas.
- Cada **Mapa Local** es una escena separada (no hay caminar de un Koro a otro sin fast travel).
- Nodos del global: no todos visibles ni accesibles al inicio; se desbloquean con la historia / flags.

---

## Cómo rellenar esto (para el autor)

Copia una fila por cada punto visitable. Lo mínimo útil es: `id`, `nombre`, `tipo`, `dónde en el mapa`, `inicio`.

**Tipos sugeridos:** `koro` (asentamiento) · `wahi` (wilderness / zona abierta) · `poi` (punto de interés puntual)

**Posición en el outline:** describe en lenguaje natural o usa un esquema 3×3 / porcentajes (ej. “sureste, sobre la península”, o `~75% X, ~70% Y` desde arriba-izquierda). La mancha blanca del arte de referencia está aprox. en el **cuadrante sureste** del cuerpo principal (antes de la península).

### Plantilla (rellena tú)

| id | nombre | tipo | región en el outline | visible_inicio | accesible_inicio | notas |
|----|--------|------|----------------------|----------------|------------------|-------|
| `nc_...` | | koro/wahi/poi | | sí/no | sí/no | |

### Ejemplo de formato (ficticio — borrar o sustituir)

| id | nombre | tipo | región en el outline | visible_inicio | accesible_inicio | notas |
|----|--------|------|----------------------|----------------|------------------|-------|
| `nc_ejemplo_koro` | (tu nombre) | koro | sureste del cuerpo principal | sí | sí | punto de partida del prólogo |
| `nc_ejemplo_wahi` | (tu nombre) | wahi | centro-norte | no | no | se revela tras flag X |

También puedes pegar la lista en el chat así:

```text
id: nc_foo
nombre: ...
tipo: koro
posición: costa oeste, a mitad de altura
visible_inicio: sí
accesible_inicio: sí
desbloquea_con: (vacío / flag / misión)
notas: ...
```

---

## Lista oficial del prólogo (autor)

| id | nombre | tipo | región en el outline | visible_inicio | accesible_inicio | notas |
|----|--------|------|----------------------|----------------|------------------|-------|
| nc_ta_wahi | Ta-Wahi | wahi | centro-este del cuerpo principal | no | no | No es un Mapa Local, es una zona. Tambien llamado Illidora Plains, unas extensas planicies |
| nc_ta_koro | Ta-Koro | koro | centro-este del cuerpo principal | sí | sí | Ubicado en medio de las planicies  |
| nc_ga_wahi | Ga-Wahi | wahi | centro-sur del cuerpo principal | no | no | No es un Mapa Local, es una zona. Tambien llamado Aldari Coast, una costa altamente comercial |
| nc_ga_koro | Ga-Koro | koro | centro-sur del cuerpo principal | sí | sí | Ubicado en la costa, es un gran asentamiento portuario |
| nc_le_wahi | Le-Wahi | wahi | sudoeste del cuerpo central | no | no | No es un Mapa Local, es una zona. Tambien llamado Aviro Glades, un claro floreado en medio de un bosque |
| nc_le_koro | Le-Koro | koro | sudoeste del cuerpo central | sí | sí | Ubicado a las afueras del bosque que rodea Aviro Glades |
| nc_po_wahi | Po-Wahi | wahi | centro-norte del cuerpo central | no | no | No es un Mapa Local, es una zona. Tambien llamado Northern Barrens, son tierras baldías y áridas |
| nc_po_koro | Po-Koro | koro | centro-norte del cuerpo central | no | no | Ubicado en un cañon en las Barrens, con casas talladas en los muros de la quebrada |
| nc_ko_wahi | Ko-Wahi | wahi | centro, ligeramente al oeste | no | no | No es un Mapa Local, es una zona. Tambien llamado Mount Rapovi, una alta montaña nevada |
| nc_ko_koro | Ko-Koro | koro | centro, ligeramente al oeste | no | no | Ubicado en lo alto del monte, en igloo-like huts |
| nc_onu_wahi | Onu-Wahi | wahi | centro, ligeramente al este | no | no | No es un Mapa Local, es una zona. Tambien llamado The Mines |
| nc_onu_koro | Onu-Koro | koro | centro, ligeramente al este | no | no | Un asentamiento minero ubicado en la entrada de The Mines, a los pies del monte Rapovi |
| nc_tr_kr_pen | Tren Krom Peninsula | wahi | sudeste del continente | no | no | No es un Mapa Local, es una zona. Una zona accidentada, con muchos acantilados y peligros por todas partes |
| nc_tiru_lake | Tiru Lake | wahi | sudeste del continente, norte de la Peninsula | no | no | No es un Mapa Local, es una zona. Un imponente lago que separa el continente de la peninsula. de él desprende un río que desemboca en el mar hacia el este |
| nc_de_wahi | De-Wahi | wahi | extremo sur de la Peninsula | no | no | No es un Mapa Local, es una zona. Una zona baldía con muy poca vida silvestre y por lo tanto muy solenciosa |
| nc_de_koro | De-Koro | koro | extremo sur de la Peninsula | no | no | Ubicada en un silencioso y rocoso acantilado |

---

## Mapas Locales ligados a cada nodo

Cuando un nodo exista, anota qué escena local usará (aunque aún no exista el archivo):

| id_nodo_global | id_escena_local (previsto) | qué es en una frase |
|----------------|----------------------------|---------------------|
| | | |
