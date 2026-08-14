# Ítems — diseño (prólogo y catálogo temprano)

**Autoría:** rellena tú. Borrador de diseño; los datos jugables irán a `content/items/` cuando se implemente inventario.

**Misiones:** [`PROLOGUE_QUESTS.md`](PROLOGUE_QUESTS.md)  
**Personajes:** [`PROLOGUE_CHARS.md`](PROLOGUE_CHARS.md)  
**Mapa:** [`PROLOGUE_MAP.md`](PROLOGUE_MAP.md)

---

## Cómo rellenar

Una fila por ítem. Prioriza lo que ya aparece en la Main (`entrega_items`, notas de quests).

**Tipos sugeridos:** `kanohi_great` · `kanohi_noble` · `kanohi_powerless` · `quest` · `key` · `misc`

| Campo | Uso |
|-------|-----|
| `id` | Estable (ej. `item_matatu_noble`) — luego irá en JSON |
| `nombre` | Nombre mostrado |
| `tipo` | Ver arriba |
| `efecto` | Qué hace en una frase (vacío si solo lore/quest) |
| `obtiene_en` | Misión / nodo / compra / botín |
| `notas` | Lore, quién lo da, restricciones |

### Plantilla

| id | nombre | tipo | efecto | obtiene_en | notas |
|----|--------|------|--------|------------|-------|
| `item_...` | | | | | |

---

## Catálogo (prólogo — rellena / amplía)

| id | nombre | tipo | efecto | obtiene_en | notas |
|----|--------|------|--------|------------|-------|
| `item_calix` | Great Kanohi Calix | kanohi_great | aumenta velocidad y stats de usuario | robada a Atuka; objetivo de recuperación | Máscara perdida de Atuka (Toa de Hielo) |
| `item_huna_noble` | Noble Kanohi Huna | kanohi_noble | invisivilidad por breve tiempo | Atuka la usa temporalmente en `pq_003` | Sustento / recuperación de energías |
| `item_matatu_noble` | Noble Kanohi Matatu | kanohi_noble | telequinesis por breve tiempo | `pq_003` (`entrega_items`) | Regalo de Atuka para ayudar en la misión |
| `item_fire_spear` | Guard Firespear | object | | cadaver de guardia matoran | Es la pista que se encuentra en el cadaver de un matoran en `nc_rock_wall` al terminar la misión `pq_004` |

---

## Estado

| Área | Estado |
|------|--------|
| Plantilla | Lista |
| Ítems citados en Main | Parcial (Calix, Huna noble, Matatu noble) |
| Efectos / poderes exactos | **Pendiente autor** |
| Volcado a `content/items/` | Cuando Fase 3 inventario |
