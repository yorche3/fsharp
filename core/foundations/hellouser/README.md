# Hello, User! — F#

Implementación de la especificación [02_Hello_User](https://yorche3.github.io/programming_languages/core/foundations/02_Hello_User/) en **F#**, ejecutado con **dotnet fsi** (F# Interactive).

Lee un nombre desde la entrada estándar y saluda al usuario.

---

## 📂 Archivos y estructura / Files & Structure

| Archivo | Propósito |
|---------|-----------|
| [`hello-user.fsx`](hello-user.fsx) | Código fuente: solicita un nombre al usuario y saluda de forma personalizada. |

**Estructura de directorios esperada:**

```text
hellouser/
├── hello-user.fsx   # Código fuente (script)
└── README.md        # Este archivo
```

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** Este proyecto usa **F#** con **F# Interactive (fsi)** y sigue un enfoque minimalista: un único archivo script `.fsx`, sin dependencias externas, ejecutado directamente con `dotnet fsi`.

Las novedades respecto a `helloworld` son:

1. **Lectura de entrada** — `System.Console.ReadLine()` lee una línea desde `stdin`.
2. **Formateo de cadenas** — `printfn "Hello, %s!" name` para construir el saludo con formato.
3. **Variables** — `name` para almacenar el nombre ingresado.
4. **Definición explícita de `main`** — función `main` que recibe `argv` y retorna un código de salida.

**EN:** This project uses **F#** with **F# Interactive (fsi)** and follows a minimalist approach: a single `.fsx` script file, no external dependencies, executed directly with `dotnet fsi`.

The new concepts compared to `helloworld` are:

1. **Input reading** — `System.Console.ReadLine()` reads a line from `stdin`.
2. **String formatting** — `printfn "Hello, %s!" name` to build the greeting with format specifiers.
3. **Variables** — `name` to store the entered name.
4. **Explicit `main` definition** — a `main` function that receives `argv` and returns an exit code.

---

## 📄 Archivos de configuración clave / Key Configuration Files

### `hello-user.fsx`

**ES:** El flujo del programa es:

1. Imprimir `"Enter your name: "` con `printf` (sin salto de línea).
2. Leer una línea con `System.Console.ReadLine()` y asignarla a la variable `name`.
3. Imprimir el saludo con `printfn` y el especificador de formato `%s`.
4. Devolver `0` como código de salida.

**EN:** Program flow:

1. Print `"Enter your name: "` with `printf` (no newline).
2. Read a line with `System.Console.ReadLine()` and assign it to the `name` variable.
3. Print the greeting with `printfn` and the `%s` format specifier.
4. Return `0` as the exit code.

```fsharp
open System 

let main argv =
    printf "Enter your name: "
    let name = System.Console.ReadLine()
    printfn "Hello, %s!" name
    0

main [||]
```

| Elemento | Propósito |
|----------|-----------|
| `open System` | Importa el namespace `System` (permite usar `Console` sin prefijo completo) |
| `let main argv =` | Define la función `main` con `argv` como argumento (arreglo de strings) |
| `printf "..."` | Imprime una cadena con formato **sin** salto de línea al final |
| `let name = ...` | Declara e inicializa la variable inmutable `name` |
| `System.Console.ReadLine()` | Lee una línea desde la entrada estándar |
| `printfn "Hello, %s!" name` | Imprime el saludo usando el formato `%s` (string) con el valor de `name` |
| `0` | Código de salida (0 = éxito) |
| `main [||]` | Invoca la función `main` pasando un arreglo vacío de strings |

> **ES:** A diferencia de C# donde `Main` es el punto de entrada automático, en un script `.fsx` de F# la función `main` debe ser invocada explícitamente al final del archivo.
> 
> **EN:** Unlike C# where `Main` is the automatic entry point, in an F# `.fsx` script the `main` function must be explicitly invoked at the end of the file.

> **ES:** `printf` (sin la `n`) no añade salto de línea, ideal para mostrar un prompt. `printfn` (con la `n`) añade el salto de línea automáticamente.
> 
> **EN:** `printf` (without the `n`) does not add a newline, ideal for displaying a prompt. `printfn` (with the `n`) adds the newline automatically.

#### Especificadores de formato en F#

**ES:** F# hereda de C los especificadores de formato con tipado seguro en `printf`/`printfn`:

| Especificador | Tipo | Ejemplo |
|--------------|------|---------|
| `%s` | `string` | `printfn "Hello, %s!" name` |
| `%d` | `int` | `printfn "Count: %d" count` |
| `%f` | `float` | `printfn "Pi: %f" 3.1415` |
| `%b` | `bool` | `printfn "Flag: %b" true` |
| `%A` | cualquier tipo | `printfn "%A" myList` (formato pretty-print) |

**EN:** F# inherits type-safe format specifiers from C for `printf`/`printfn`:

| Specifier | Type | Example |
|-----------|------|---------|
| `%s` | `string` | `printfn "Hello, %s!" name` |
| `%d` | `int` | `printfn "Count: %d" count` |
| `%f` | `float` | `printfn "Pi: %f" 3.1415` |
| `%b` | `bool` | `printfn "Flag: %b" true` |
| `%A` | any type | `printfn "%A" myList` (pretty-print format) |

> **ES:** A diferencia de C, F# verifica en **tiempo de compilación** que los especificadores de formato coincidan con los tipos de los argumentos, previniendo errores clásicos de `printf`.
> 
> **EN:** Unlike C, F# checks at **compile time** that format specifiers match the argument types, preventing classic `printf` errors.

---

## 🚀 Compilación y ejecución / Build & Run

### Requisito: Tener .NET SDK instalado (incluye F#)

```bash
dotnet --version   # Verificar instalación
```

### Ejecutar como script (recomendado)

```bash
cd core/foundations/hellouser
dotnet fsi hello-user.fsx
```

**Salida esperada / Expected output:**

```text
Enter your name: Ada
Hello, Ada!
```

> **ES:** El programa espera a que el usuario escriba su nombre y presione Enter antes de mostrar el saludo.
> **EN:** The program waits for the user to type their name and press Enter before showing the greeting.

---

## 📝 Notas de implementación / Implementation Notes

- **ES:** En scripts `.fsx` se usa `System.Console.ReadLine()` explícitamente. `open System` permite abreviar a `Console.ReadLine()`.
- **EN:** In `.fsx` scripts, `System.Console.ReadLine()` is used explicitly. `open System` allows shortening it to `Console.ReadLine()`.
- **ES:** Se invoca `main [||]` al final porque los scripts `.fsx` no tienen un punto de entrada automático como los proyectos compilados.
- **EN:** `main [||]` is called at the end because `.fsx` scripts don't have an automatic entry point like compiled projects.
- **ES:** F# usa `let` para declarar variables, que por defecto son **inmutables** (no reasignables). Para mutabilidad se usa `let mutable`.
- **EN:** F# uses `let` to declare variables, which are **immutable** by default (cannot be reassigned). Use `let mutable` for mutability.
- **ES:** El arreglo vacío `[||]` representa un array de strings vacío en F#. La sintaxis `[| ... |]` es propia de F# para arrays.
- **EN:** The empty array `[||]` represents an empty string array in F#. The `[| ... |]` syntax is F#-specific for arrays.

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
