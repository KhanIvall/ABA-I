# Prólogo — Misiones (`ch0_prologue`)

**Autoría:** rellena tú. Este archivo es el borrador de diseño; el grafo jugable irá a `content/quests/` cuando se implemente.

**Mapa / nodos:** [`PROLOGUE_MAP.md`](PROLOGUE_MAP.md)  
**Personajes:** [`PROLOGUE_CHARS.md`](PROLOGUE_CHARS.md)

---

## Premisa (conocida)

- Capítulo: `ch0_prologue` — **Search for Power**
- Jugable: **Toa Lhikan**.
- **Escena inicial:** `local_ga_koro` (`nc_ga_koro`) — Lhikan llega al puerto tras un viaje.
- Estructura: **una Main** + **eventos de mapa** (no side-quests lineales en paralelo).

**Diseño relacionado:** [`PROLOGUE_ITEMS.md`](PROLOGUE_ITEMS.md) (ítems / Kanohi del prólogo)

---

## Cómo rellenar

### Main (obligatorio antes de implementar el prólogo)

La Kanohi Calix del Toa de Hielo Atuka se ha perdido. Ahora él yace en elcentro de Aviro Glades, sobreviviendo a duras penas con una Kanohi noble que simplemente le da el sustento necesario para seguir vivo.

Una fila por beat / misión del hilo principal. Orden = secuencia aproximada.

`entrega_items`: ids de ítems que el jugador **recibe al completar** la misión (separados por coma; usar ids de [`PROLOGUE_ITEMS.md`](PROLOGUE_ITEMS.md)). Vacío si no hay.

| orden | id   | título (trabajo) | objetivo en una frase             | id_nodo / escena | desbloquea (nodo/flag/misión) | entrega_items | notas |
|------:|------|------------------|-----------------------------------|------------------|-------------------------------|---------------|-------|
| 1 | `pq_001` | Busca a Taruhi | dirigirse hacia donde está Taruhi | `nc_ga_koro` / `local_ga_koro` | `pq_002` | | llegada al puerto. Matoran de confianza, Taruhi. Después de la bienvenida la matoran pide que asista a Turaga Volog con un problema reciente |
| 2 | `pq_002` | Habla con Volog | viajar a Le-Koro para hablar con Volog | `nc_le_koro` / `local_le_koro` | `pq_003` | | Volog le cuenta a Lhikan que Matoran reportaron una batalla en los Claros y recientemente unas figuras saltaron el muro que rodea el bosque |
| 3 | `pq_003` | Ayuda al Toa Caido | encuentra y asiste al Toa caido | `nc_glade` / `local_glade` | `pq_004`, `f_acc_road` | `item_matatu_noble` | al llegar debe defender al Toa de unos Rahi. Atuka se puso una Noble Kanohi Huna (`item_huna_noble`) que llevaba para recuperar energías. Solicita ayuda para recuperar su Kanohi robada (`item_calix_great`), y regala una `item_matatu_noble` para ayudar con la mision |
| 4 | `pq_004` | Sigue las huellas | Sigue el rastro de huellas hasta encontrar alguna pista | `nc_` / `local_` | `pq_005` | `item_fire_spear` | usar mecánica de busqueda para guiarse por las huellas que se vayan encontrando hasta llegar al objetivo |
| 5 | `pq_005` | Investiga en Ta-Koro | Ve a Ta-Koro a investigar la muerte del Ta-Matoran | `nc_ta_koro` / `local_ta_koro` | `pq_006` | | al encontrar el cadaver de un guardia Ta-Matoran al final del rastro de huellas hay que investigar en Ta-Koro. Se completa al hablar con el Turaga de Ta-Koro |
| 6 | `pq_006` | Investiga en el norte | ve a Po-Koro a buscar a los criminales | `nc_` / `local_` | | | Turaga Ferin cuenta que el Matoran muerto era un guardia de Ta-Koro. Otros guardias informaron haber visto Skakdi moviendose hacia el norte a Po-Koro |
| 7 | `pq_007` |  |  | `nc_` / `local_` | | |  |

### Recorrido Global de Main Quest

- Lhikan llega al continente y es informado de problemas en `Le-Wahi`
- Ahí se entera de una batalla que tomó lugar en el Claro
- Al llegar encuentra un Toa (`Atuka`) al cual le robaron su mascara unos criminales de Stelt
- Sigue el rastro hasta `Ta-Koro` donde aseguran haberlos visto moviendose hacia `Po-Koro`
- Al llegar a `Po-Koro` un grupo de matoran pide ayuda alegando que su Toa estaba siendo atacado en una playa rocosa
- Los criminales habían vencido al Toa (`Motara`) y robado su máscara. Dos arrancan y dejan a un Skakdi peleando con Lhikan
- Luego de conseguirse una Kanohi Noble para estabilizar al Toa, éste le cuenta que los criminales estan buscando una Kanohi Zuade
- El único Toa que posée una Zuade es el que resguarda el Monte Rapovi (`Andoremo`). Lhikan se dirige a `Onu-Wahi`, donde está la entrada más accesible al monte.
- Al llegar los locales de `Onu-Koro` le advierten que subir por fuera es más peligroso, ya que significa rodear el monte por horas, mientras que subir por el complejo de cuevas es más directo. Además acababan de ver un par de criminales subir por el camino largo.
- Si Lhikan llega por el camino corto (cuevas) advierte al Toa del inminente ataque y organizan una emboscada.
- Emboscan al Steltian y el Skakdi. Andoremo pelea contra el Steltian y Lhikan contra el Skakdi.
- Andoremo es derrotado y el Steltian se lleva su máscara, iniciando una persecución bajando la montaña, el criminal con un trineo robado de `Ko-Koro` y Lhikan en sus Fire Greatswords
- Recupera las 3 Kanohi, y en ese preciso instante llega un matoran con un Vahki para reclutarlo para ayudar en Metru Nui
- Al devolverle las Kanohi a sus propietarios aprovecha de reclutarlos. El punto de reunion es la playa al norte de `Po-Wahi`
- El capítulo termina al reunirse los 11 Toa Mangai en el bote

### Eventos de mapa (opcionales / mundo vivo)

Encuentros, hallazgos, suva, etc. No compiten con la Main.

| id | dónde (`id_nodo`) | trigger (cómo aparece) | qué pasa en una frase | recompensa / flag | notas |
|----|-------------------|------------------------|-----------------------|-------------------|-------|
| `pe_` |  | | | | |

### Flags previstos (borrador)

Lista corta de flags que la Main / eventos / nodos van a necesitar. Evita inventar docenas; solo los que ya sepas.

| flag               | qué significa           | lo pone          | lo usa |
|--------------------|-------------------------|------------------|--------|
| `f_unlock_le_01` | se desbloquea `area_le_01` | nodo `nc_ga_le_korin` completado | `area_le_01` |
| `f_acc_road` | el acceso a Gran Camino se vuelve accesible | `pq_003` | `nc_collapse` |
| `f_unlock_road_01` | se desbloquea `area_road_01` | nodo `nc_collapse` completado | `area_road_01` |

---

## Estado

| Área | Estado |
|------|--------|
| Escena inicial | Definida (`local_ga_koro`) |
| Main (beats) | **Pendiente autor** |
| Eventos de mapa | **Pendiente autor** |
| Flags | **Pendiente autor** |
| Volcado a `quests.json` | Después de diseñar (Fase 4 / cuando indiques) |
