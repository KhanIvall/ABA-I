# ABA-I — notas para agentes / colaboradores

## Git

**No hacer `commit` ni `push` sin que el dueño del repo lo pida explícitamente.**

- Pedidos válidos: "haz commit", "commit y push", "sube esto a GitHub".
- No válidos como autorización: terminar una feature, "perfecto, funciona", arreglar un bug, actualizar el plan.

**Rama de trabajo:** usar **`dev`**. No pushear directo a `main`/`master` salvo petición explícita.

Reglas Cursor: [`.cursor/rules/git-commit-push.mdc`](.cursor/rules/git-commit-push.mdc), [`.cursor/rules/git-branch-dev.mdc`](.cursor/rules/git-branch-dev.mdc).

## Diseño e historia

El dueño del proyecto **diseña** misiones, ítems, personajes y locaciones. Ver [`DESIGN_INTENT.md`](DESIGN_INTENT.md).

- Adaptación **fiel al canon**; licencias creativas solo en huecos/inconsistencias (mínimas).
- No inventar contenido Bionicle / misiones “de relleno”.
- Placeholders técnicos genéricos sí; lore y beats narrativos solo con petición o texto del autor.
- Mapa Global (overworld por áreas + nexos `pass`) + Mapa Local (escena al entrar en un destino). Detalle: [`PROLOGUE_MAP.md`](content/design/PROLOGUE_MAP.md).
- Nodos del prólogo: [`content/design/PROLOGUE_MAP.md`](content/design/PROLOGUE_MAP.md); misiones/personajes: [`PROLOGUE_QUESTS.md`](content/design/PROLOGUE_QUESTS.md), [`PROLOGUE_CHARS.md`](content/design/PROLOGUE_CHARS.md).
- Regla Cursor: [`.cursor/rules/design-authorship.mdc`](.cursor/rules/design-authorship.mdc).
