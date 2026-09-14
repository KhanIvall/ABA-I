# Prólogo — Misiones (`ch0_prologue`)

**Autoría:** rellena tú. Este archivo es el borrador de diseño; el grafo jugable irá a `content/quests/` cuando se implemente.

**Mapa / nodos:** `[PROLOGUE_MAP.md](PROLOGUE_MAP.md)`  
**Personajes:** `[PROLOGUE_CHARS.md](PROLOGUE_CHARS.md)`

---

## Premisa (conocida)

- Capítulo: `ch0_prologue` — **Search for Power**
- Jugable: **Toa Lhikan**.
- **Escena inicial:** `local_ga_koro` (`nc_ga_koro`) — Lhikan llega al puerto tras un viaje.
- Estructura: **una Main** + **eventos de mapa** (no side-quests lineales en paralelo).

**Diseño relacionado:** `[PROLOGUE_ITEMS.md](PROLOGUE_ITEMS.md)` (ítems / Kanohi del prólogo)

---



## Cómo rellenar



### Main (obligatorio antes de implementar el prólogo)

La Kanohi Calix del Toa de Hielo Atuka se ha perdido. Ahora él yace en el centro de Aviro Glades, sobreviviendo a duras penas con una Kanohi noble que simplemente le da el sustento necesario para seguir vivo.

Una fila por beat / misión del hilo principal. Orden = secuencia aproximada.

**Ids (una Main por jugable):**
- `pq_lk_*` — **Lhikan** (esta tabla; primera a definir)
- `pq_nk_*` — **Nidhiki** (pendiente)
- `pq_nh_*` — **Naho** (pendiente)

`entrega_items`: ids de ítems que el jugador **recibe al completar** la misión (separados por coma; usar ids de `[PROLOGUE_ITEMS.md](PROLOGUE_ITEMS.md)`). Vacío si no hay.


| orden | id       | título (trabajo)         | objetivo en una frase                                   | id_nodo / escena                   | desbloquea (nodo/flag/misión)    | entrega_items       | notas                                                                                                                                                                                                                                                                     |
| ----- | -------- | ------------------------ | ------------------------------------------------------- | ---------------------------------- | -------------------------------- | ------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1     | `pq_lk_001` | Busca a Taruhi        | dirigirse hacia donde está Taruhi                       | `nc_ga_koro` / `local_ga_koro`     | `pq_lk_002`                      |                     | llegada al puerto. Matoran de confianza, Taruhi. Después de la bienvenida la matoran pide que asista a Turaga Volog con un problema reciente                                                                                                                              |
| 2     | `pq_lk_002` | Habla con Volog       | viajar a Le-Koro para hablar con Volog                  | `nc_le_koro` / `local_le_koro`     | `pq_lk_003`                      |                     | ve a Le-Koro. Termina al hablar con Volog                                                                                                                                                                                                                                 |
| 3     | `pq_lk_003` | Ayuda al Toa caído    | encuentra y asiste al Toa caido                         | `nc_glade` / `local_glade`         | `pq_lk_004`, `f_acc_road`        | `item_matatu_noble` | Volog te cuenta que Matoran reportaron una batalla en los Claros y recientemente unas figuras saltaron el muro que rodea el bosque. Al llegar debe defender al Toa de unos Rahi. Atuka se puso una Noble Kanohi Huna (`item_huna_noble`) que llevaba para recuperar energías. Se completa al matar a los Rahi |
| 4     | `pq_lk_004` | Sigue las huellas     | sigue el rastro de huellas hasta encontrar alguna pista | `nc_rock_wall` / `local_rock_wall` | `pq_lk_005`                      | `item_fire_spear`   | el Toa solicita ayuda para recuperar su Kanohi robada (`item_calix_great`), y regala una Kanohi `item_matatu_noble` para ayudar con la mision. usar mecánica de busqueda para guiarse por las huellas que se vayan encontrando hasta llegar al objetivo. Se completa al encontrar el cadaver del Matoran |
| 5     | `pq_lk_005` | Investiga en Ta-Koro  | ve a Ta-Koro a investigar la muerte del Ta-Matoran      | `nc_ta_koro` / `local_ta_koro`     | `pq_lk_006`                      |                     | al encontrar el cadaver de un guardia Ta-Matoran al final del rastro de huellas hay que investigar en Ta-Koro. Se completa al hablar con el Turaga de Ta-Koro                                                                                                             |
| 6     | `pq_lk_006` | Investiga en el norte | ve al norte a buscar a los criminales                   | `nc_cliff` / `local_cliff`         | `pq_lk_007`                      | (por definir)       | al llegar al acantilado siguiendo las huellas, dos Air Serpent acechan con botar por el acantilado a tres matoran, guardias de Ta-Koro. Se completa al vencer a los Rahi                                                                                                  |
| 7     | `pq_lk_007` | Alcanza a los criminales | dirigete a Po-Koro y alcanza a los criminales        | `nc_po_koro` / `local_po_koro`     | `pq_lk_008`                      |                     | los tres guardias que salvaste (dos cayeron por el acantilado) te dicen que los criminales huyeron por la cascada hacia Po-Koro. Debes ir a buscarlos antes de que cuasen más problemas. Se completa al hablar con el Turaga de Po-Koro                                   |
| 8     | `pq_lk_008` | Ayuda a Motara        | dirigete a la playa y ayuda a Motara                    | `nc_beach` / `local_beach`         | `pq_lk_009`                      |                     | El Turaga pide que ayude a Toa Motara que se enfrenta con los criminales en la playa del norte. Al llegar Motara fue derrotado, los ves escapar de unos Rahi voladores por entre las rocas, al seguirlos un Skakdi se devuelve para enfrentarte. Termina al vencerlo      |
| 9     | `pq_lk_009` | Consigue una Kanohi   | trae una Kanohi para estabilizar a Motara               | `nc_` / `local_`                   | `pq_lk_010`                      |                     |                                                                                                                                                                                                                                                                           |
| 10    | `pq_lk_010` |                       |                                                         | `nc_` / `local_`                   | `pq_lk_011`                      |                     |                                                                                                                                                                                                                                                                           |

### Recorrido Global de Main Quest

- [x] Lhikan llega al continente y es informado de problemas en `Le-Wahi`
- [x] Ahí se entera de una batalla que tomó lugar en el Claro
- [x] Al llegar encuentra un Toa (`Atuka`) al cual le robaron su mascara unos criminales de Stelt
- [x] Sigue el rastro hasta `Ta-Koro` donde aseguran haberlos visto moviendose hacia el norte
- [x] Al llegar a `Po-Koro` el Turaga le pide ayuda alegando que su Toa estaba siendo atacado en una playa rocosa
- [x] Los criminales habían vencido al Toa (`Motara`) y robado su máscara. Dos arrancan y dejan a un Skakdi peleando con Lhikan
- [ ] Luego de conseguirse una Kanohi Noble para estabilizar al Toa, éste le cuenta que los criminales estan buscando una Kanohi Zaude
- [ ] El único Toa que posee una Zaude es el que resguarda el Monte Rapovi (`Andoremo`). Lhikan se dirige a `Onu-Wahi`, donde está la entrada más accesible al monte.
- [ ] Al llegar los locales de `Onu-Koro` le advierten que subir por fuera es más peligroso, ya que significa rodear el monte por horas, mientras que subir por el complejo de cuevas es más directo. Además acababan de ver un par de criminales subir por el camino largo.
- [ ] Si Lhikan llega por el camino corto (cuevas) advierte al Toa del inminente ataque y organizan una emboscada.
- [ ] Emboscan al Steltian y el Skakdi. Andoremo pelea contra el Steltian y Lhikan contra el Skakdi.
- [ ] Andoremo es derrotado y el Steltian se lleva su máscara, iniciando una persecución bajando la montaña, el criminal con un trineo robado de `Ko-Koro` y Lhikan en sus Fire Greatswords
- [ ] Recupera las 3 Kanohi, y en ese preciso instante llega un matoran con un Vahki para reclutarlo para ayudar en Metru Nui
- [ ] Al devolverle las Kanohi a sus propietarios aprovecha de reclutarlos. El punto de reunion es la playa al norte de `Po-Wahi`
- [ ] El capítulo termina al reunirse los 11 Toa Mangai en el bote


### Eventos de mapa (opcionales / mundo vivo)

Encuentros, hallazgos, suva, etc. No compiten con la Main.


| id               | dónde (`id_nodo`) | trigger (cómo aparece)                | qué pasa en una frase                                 | recompensa / flag | notas |
| ---------------- | ----------------- | ------------------------------------- | ----------------------------------------------------- | ----------------- | ----- |
| `pe_broken_gate` | `nc_watch_tower`  | Al hablar con ``, guardia de la torre | es posible ayudar a reconstruir la puerta con piedras |                   |       |

### Flags previstos (borrador)

Lista corta de flags que la Main / eventos / nodos van a necesitar. Evita inventar docenas; solo los que ya sepas.


| flag               | qué significa                               | lo pone                          | lo usa         |
| ------------------ | ------------------------------------------- | -------------------------------- | -------------- |
| `f_unlock_le_01`   | se desbloquea `area_le_01`                  | nodo `nc_korin` completado       | `area_le_01`   |
| `f_acc_road`       | el paso al Gran Camino se vuelve accesible  | `pq_lk_003`                      | `nc_collapse`  |
| `f_unlock_road_01` | se desbloquea `area_road_01`                | nodo `nc_collapse` completado    | `area_road_01` |


---



## Estado


| Área                    | Estado                                        |
| ----------------------- | --------------------------------------------- |
| Escena inicial          | Definida (`local_ga_koro`)                    |
| Main (beats)            | **Pendiente autor**                           |
| Eventos de mapa         | **Pendiente autor**                           |
| Flags                   | **Pendiente autor**                           |
| Volcado a `quests.json` | Después de diseñar (Fase 4 / cuando indiques) |
