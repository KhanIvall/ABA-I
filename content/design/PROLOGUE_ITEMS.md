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

| id | nombre | descripción | tipo | efecto | obtiene_en | notas |
|----|--------|-------------|------|--------|------------|-------|
| `item_hau_great` | Great Kanohi Hau | Great Mask of Shielding | kanohi_great | escudo protector |  | Mascara de Toa Lhikan `pc_lhikan` |
| `item_ruru_great` | Great Kanohi Ruru | Great Mask of Night Vision | kanohi_great | vision nocturna (siempre activa) |  | Mascara de Toa Naho `pc_naho` |
| `item_volitak_great` | Great Kanohi Volitak | Great Mask of Stealth | kanohi_great | sigilo y camuflage |  | Mascara de Toa Nidihki `pc_nidhiki` |
| `item_calix_great` | Great Kanohi Calix | Great Mask of Fate | kanohi_great | aumenta velocidad y stats de usuario | robada a Atuka; objetivo de recuperación | Máscara perdida de Toa Atuka `npc_atuka` |
| `item_huna_noble` | Noble Kanohi Huna | Noble Mask of Concealment | kanohi_noble | invisivilidad por breve tiempo | Atuka la usa temporalmente en `pq_003` | Sustento / recuperación de energías para Toa Atuka `npc_atuka` y Matoran Taruhi `npc_001` |
| `item_matatu_noble` | Noble Kanohi Matatu | Noble Mask of Telekinesis | kanohi_noble | telequinesis por breve tiempo | `pq_003` (`entrega_items`) | Regalo de Atuka para ayudar en la misión |
| `item_undara_great` | Great Kanohi Undara | Great Mask of Intangibility | kanohi_great | otorga intangibilidad |  | Mascara de Toa Tuyet `npc_tuyet` |
| `item_zaude_great` | Great Kanohi Zaude | Great Mask of Conjuring | kanohi_great | imita a otras kanohi, pero con un backlash |  | Mascara de Toa Andoremo `npc_andoremo` |
| `item_rode_great` | Great Kanohi Rode | Great Mask of Truth | kanohi_great | ver a través de trucos e ilusiones |  | Mascara de Toa Ziid `npc_ziid` |
| `item_kaukau_great` | Great Kanohi Kaukau | Great Mask of Water Breathing | kanohi_great | respirar bajo el agua |  | Mascara de Toa Froti `npc_froti` |
| `item_iridus_great` | Great Kanohi Iridus | Great Mask of Explosions | kanohi_great | produce explosiones a voluntad |  | Mascara de Toa Motara `npc_motara` |
| `item_kakama_great` | Great Kanohi Kakama | Great Mask of Speed | kanohi_great | aumenta velocidad |  | Mascara de Toa Ruados `npc_ruados` |
| `item_nuvi_great` | Great Kanohi Nuvi | Great Mask of Healing | kanohi_great | cura heridas de terceros |  | Mascara de Toa Taibo `npc_taibo` |
| `item_kakama_noble` | Noble Kanohi Kakama | Noble Mask of Speed | kanohi_noble | aumenta velocidad por breve tiempo |  | Mascara de Turaga Volog `npc_002` |
| `item_akaku_noble` | Noble Kanohi Akaku | Noble Mask of X Ray Vision | kanohi_noble | otorga visión de rayos x por breve tiempo |  | Mascara de Turaga Ferin `npc_003` |
| `item_fire_spear` | Guard Firespear | Weapon of the Ta-Koro guards | object | - | cadaver de guardia matoran | Es la pista que se encuentra en el cadaver de un matoran en `nc_rock_wall` al terminar la misión `pq_004` |

---

## Estado

| Área | Estado |
|------|--------|
| Plantilla | Lista |
| Ítems citados en Main | Parcial (Calix, Huna noble, Matatu noble) |
| Efectos / poderes exactos | **Pendiente autor** |
| Volcado a `content/items/` | Cuando Fase 3 inventario |
