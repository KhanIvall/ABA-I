# Prólogo — Personajes (`ch0_prologue`)

**Autoría:** rellena tú. No hace falta ficha completa de todos; prioriza quienes aparecen en la Main y en `local_ga_koro` al inicio.

**Mapa / nodos:** [`PROLOGUE_MAP.md`](PROLOGUE_MAP.md)  
**Misiones:** [`PROLOGUE_QUESTS.md`](PROLOGUE_QUESTS.md)

---

## Jugable

| id | nombre | rol | notas |
|----|--------|-----|-------|
| `pc_lhikan` | Toa Lhikan | jugador | Llega a Ga-Koro por puerto al inicio del prólogo |

---

## NPCs / otros (rellena)

Una fila por personaje con diálogo o función en el prólogo. `id_nodo` = dónde suele estar (puede moverse por beats).

| id | nombre | tipo | id_nodo (base) | rol en una frase | aparece_inicio | notas |
|----|--------|------|----------------|------------------|----------------|-------|
| `npc_001` | Taruhi | `matoran` | `nc_ga_koro` | orienta a Lhikan tras llegar. Forma parte de Main quest | sí | |
| `npc_002` | Volog | `turaga` | `nc_le_koro` | informa del altercado en el Claro. Forma parte de Main quest | sí | |
| `npc_003` | Atuka | `toa` | `nc_glade` | pide ayuda para recuperar su Kanohi. Forma parte de Main quest | sí | Perdió su Kanohi en una batalla con unos criminales (Un Steltian y dos Skakdi) |

**Tipos sugeridos:** `matoran` · `toa` · `turaga` · `rahkshi` · `otro`

---

## Vínculos rápidos (opcional)

Quién importa en qué beat de la Main (ids de `PROLOGUE_QUESTS.md`).

| id_misión (`pq_...`) | personajes implicados (`id`) | nota |
|----------------------|------------------------------|------|
| `pq_001` | `npc_001` | termina la quest |
| `pq_002` | `npc_001` | da la quest |
| `pq_002` | `npc_002` | termina la quest |
| `pq_003` | `npc_002` | da la quest |
| `pq_003` | `npc_003` | termina la quest |

---

## Estado

| Área | Estado |
|------|--------|
| Jugable | Lhikan |
| NPCs Ga-Koro (inicio) | **Pendiente autor** |
| Resto del prólogo | **Pendiente autor** |
