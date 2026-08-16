# Prólogo — Personajes (`ch0_prologue`)

**Autoría:** rellena tú. No hace falta ficha completa de todos; prioriza quienes aparecen en la Main y en `local_ga_koro` al inicio.

**Mapa / nodos:** [`PROLOGUE_MAP.md`](PROLOGUE_MAP.md)  
**Misiones:** [`PROLOGUE_QUESTS.md`](PROLOGUE_QUESTS.md)  
**Ítems:** [`PROLOGUE_ITEMS.md`](PROLOGUE_ITEMS.md)

---

## Jugable

| id | nombre | rol | notas |
|----|--------|-----|-------|
| `pc_lhikan` | Toa Lhikan | jugador | Llega a Ga-Koro por puerto al inicio del prólogo. Porta una Great Kanohi Hau `item_hau_great` |
| `pc_naho` | Toa Naho | jugador | Investiga el secuestro de Toas en Stelt. Porta una Great Kanohi Ruru `item_ruru_great` |
| `pc_nidhiki` | Toa Nidhiki | jugador | Porta una Great Kanohi Volitak `item_volitak_great` |

---

## NPCs / otros (rellena)

Una fila por personaje con diálogo o función en el prólogo. `id_nodo` = dónde suele estar (puede moverse por beats).

| id | nombre | tipo | id_nodo (base) | rol en una frase | aparece_inicio | notas |
|----|--------|------|----------------|------------------|----------------|-------|
| `npc_tuyet` | Tuyet | `toa` |  |  | no | Toa de Agua. Porta una Great Kanohi Undara `item_undara_great` |
| `npc_atuka` | Atuka | `toa` | `nc_glade` | pide ayuda para recuperar su Kanohi. Forma parte de Main quest | no | Toa de Hielo. Perdió su Great Kanohi Calix `item_calix_great` en una batalla con unos criminales (Un Steltian y dos Skakdi). Usa una Noble Kanohi Huna `item_huna_noble` para conservar energías |
| `npc_andoremo` | Andoremo | `toa` |  |  | no | Toa de Hielo. Porta una Great Kanohi Zaude `item_zaude_great` |
| `npc_ziid` | Ziid | `toa` |  |  | no | Toa de Hielo. Porta una Great Kanohi Rode `item_rode_great` |
| `npc_froti` | Froti | `toa` |  |  | no | Toa de Hielo. Porta una Great Kanohi Kaukau `item_kaukau_great` |
| `npc_motara` | Motara | `toa` |  |  | no | Toa de Piedra. Porta una Great Kanohi Iridus `item_iridus_great` |
| `npc_ruados` | Ruados | `toa` |  |  | no | Toa de Tierra. Porta una Great Kanohi Kakama `item_kakama_great`. Viene del sur del Universo Matoran con su fiel amigo Taibo |
| `npc_taibo` | Taibo | `toa` |  |  | no | Toa de lo Verde. Porta una Great Kanohi Nuvi `item_nuvi_great`. Viene del sur del Universo Matoran con su fiel amigo Ruados |
| `npc_001` | Taruhi | `matoran` | `nc_ga_koro` | orienta a Lhikan tras llegar. Forma parte de Main quest | sí | Porta una Noble Kanohi Huna `item_huna_noble` |
| `npc_002` | Volog | `turaga` | `nc_le_koro` | informa del altercado en el Claro. Forma parte de Main quest | sí | Porta una Noble Kanohi Kakama `item_kakama_noble` |

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
| `pq_003` | `npc_atuka` | termina la quest |

---

## Estado

| Área | Estado |
|------|--------|
| Jugable | Lhikan |
| NPCs Ga-Koro (inicio) | **Pendiente autor** |
| Resto del prólogo | **Pendiente autor** |
