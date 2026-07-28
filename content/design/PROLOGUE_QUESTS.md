# Prólogo — Misiones (`ch0_prologue`)

**Autoría:** rellena tú. Este archivo es el borrador de diseño; el grafo jugable irá a `content/quests/` cuando se implemente.

**Mapa / nodos:** [`PROLOGUE_MAP.md`](PROLOGUE_MAP.md)  
**Personajes:** [`PROLOGUE_CHARS.md`](PROLOGUE_CHARS.md)

---

## Premisa (conocida)

- Capítulo: `ch0_prologue` — Northern Continent, pre-reclutamiento.
- Jugable: **Toa Lhikan**.
- **Escena inicial:** `local_ga_koro` (`nc_ga_koro`) — Lhikan llega al puerto tras un viaje.
- Estructura: **una Main** + **eventos de mapa** (no side-quests lineales en paralelo).

---

## Cómo rellenar

### Main (obligatorio antes de implementar el prólogo)

Una fila por beat / misión del hilo principal. Orden = secuencia aproximada.

| orden | id | título (trabajo) | objetivo en una frase | id_nodo / escena | desbloquea (nodo/flag/misión) | notas |
|------:|----|------------------|-----------------------|------------------|-------------------------------|-------|
| 1 | `pq_001` | Busca a Taruhi | dirigirse hacia donde está Taruhi | `nc_ga_koro` / `local_ga_koro` | `pq_002` | llegada al puerto |
| 2 | `pq_002` | Habla con Volog | viajar a Le-Koro para hablar con Volog | `nc_le_koro` / `local_le_koro` | `pq_003` |  |
| 3 | `pq_003` | | | | | |

### Eventos de mapa (opcionales / mundo vivo)

Encuentros, hallazgos, suva, etc. No compiten con la Main.

| id | dónde (`id_nodo`) | trigger (cómo aparece) | qué pasa en una frase | recompensa / flag | notas |
|----|-------------------|------------------------|-----------------------|-------------------|-------|
| `pe_...` | | | | | |

### Flags previstos (borrador)

Lista corta de flags que la Main / eventos / nodos van a necesitar. Evita inventar docenas; solo los que ya sepas.

| flag | qué significa | lo pone | lo usa |
|------|---------------|---------|--------|
| | | | |

---

## Estado

| Área | Estado |
|------|--------|
| Escena inicial | Definida (`local_ga_koro`) |
| Main (beats) | **Pendiente autor** |
| Eventos de mapa | **Pendiente autor** |
| Flags | **Pendiente autor** |
| Volcado a `quests.json` | Después de diseñar (Fase 4 / cuando indiques) |
