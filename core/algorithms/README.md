# Algorithms Pure — F#

Implementaciones de la [Fase 1 — Algoritmos Puros](https://yorche3.github.io/programming_languages/ROADMAP/#fase-1--algoritmos-puros--algorithms-pure-) en **F# (.NET 10)**: ordenamientos elementales, estructuras de datos propias, ordenamientos óptimos y distribuidos, y búsqueda.

Los módulos de esta fase trabajan sobre listas **inmutables** (`int list`): ninguna función ordena *in-place*, todas devuelven una lista nueva.

---

## 📋 Módulos

| # | Módulo | Descripción | Tests |
|---|--------|-------------|:-----:|
| [05](naive_sort/) | [Naive Sort](https://yorche3.github.io/programming_languages/core/algorithms/05_Naive_Sort/) | Selection, Bubble e Insertion Sort ($O(n^2)$) | 3 |

---

## 📂 Estructura

```text
algorithms/
└── naive_sort/                  # 05_Naive_Sort
    ├── NaiveSort.slnx           # Solución .NET
    ├── src/
    │   ├── NaiveSort.fs         # selectionSort, bubbleSort, insertionSort
    │   └── NaiveSort.fsproj
    ├── test/
    │   ├── NaiveSortTests.fs    # 3 tests (7 casos cada uno)
    │   └── NaiveSort.Tests.fsproj
    └── README.md
```

---

## 🛠️ Patrón común / Common Pattern

| Característica | Descripción |
|---------------|-------------|
| **Runtime** | .NET 10 (F# incluido en el SDK) |
| **Estructura** | Solución `.slnx` con proyecto de biblioteca (`src/`) y de tests (`test/`) |
| **Proyecto fuente** | `classlib` de F# con `<Compile Include="...fs" />` |
| **Tests** | xUnit (`[<Fact>]`, `Assert.Equal`, `Assert.True`) |
| **Entrada** | `dotnet test <solucion>.slnx` |
| **Helpers** | Funciones **locales** dentro de cada función pública (encapsulación) |
| **Inmutabilidad** | Listas inmutables: las funciones devuelven una lista nueva |
| **Indicador de fallo** | No aplica: `int list` no admite `null` |
| **Artefactos** | `bin/`, `obj/` — cubiertos por el `.gitignore` de la raíz del submódulo |

---

## 🚀 Compilación rápida / Quick Build

```bash
# Naive Sort Tests
cd naive_sort
dotnet test NaiveSort.slnx
```

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

## ▶️ Siguiente / Next

👉 Continúa con los módulos pendientes de esta fase en el [Roadmap](https://yorche3.github.io/programming_languages/ROADMAP/).
👉 Continue with the pending modules of this phase in the [Roadmap](https://yorche3.github.io/programming_languages/ROADMAP/).

---

*[← Volver a Core](../README.md)*

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
