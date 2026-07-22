# Prólogo — Northern Continent (mapa y nodos)

Referencia visual (silueta tierra/agua): [`northern_continent_outline.png`](northern_continent_outline.png)

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

*(Vacía a propósito — rellenar cuando tengas la disposición de Wahi/Koro.)*

| id | nombre | tipo | región en el outline | visible_inicio | accesible_inicio | notas |
|----|--------|------|----------------------|----------------|------------------|-------|
| | | | | | | |

---

## Mapas Locales ligados a cada nodo

Cuando un nodo exista, anota qué escena local usará (aunque aún no exista el archivo):

| id_nodo_global | id_escena_local (previsto) | qué es en una frase |
|----------------|----------------------------|---------------------|
| | | |
