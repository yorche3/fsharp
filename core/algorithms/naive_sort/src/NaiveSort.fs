module NaiveSort

// Esqueleto del módulo `naive_sort` — especificación 05_Naive_Sort.
//
// Contrato (int list -> int list), de menor a mayor:
//   selectionSort   — encuentra el mínimo del tramo no ordenado
//   bubbleSort      — compara e intercambia adyacentes, con bandera `swapped`
//   insertionSort   — inserta cada elemento en su sub-array ordenado
//
// Caso nulo: F# no permite el literal `null` para `int list` y no existe
// representación de lista inválida, por lo que el caso de la especificación
// se omite.
let rec selectionSort arr =
    let rec pickMin minValue acc rest =
        match rest with
        | [] -> (minValue, List.rev acc)
        | x::xs ->
            if x < minValue then
                pickMin x (minValue::acc) xs
            else
                pickMin minValue (x::acc) xs

    match arr with
    | [] -> []
    | [x] -> [x]
    | first::rest ->
        let minValue, restWithoutMin = pickMin first [] rest
        minValue :: selectionSort restWithoutMin

let rec bubbleSort arr =
    let rec bubblePass acc swapped rest =
        match rest with
        | [] -> (List.rev acc, swapped)
        | [x] -> (List.rev (x::acc), swapped)
        | x::y::xs ->
            if x > y then
                bubblePass (y::acc) true (x::xs)
            else
                bubblePass (x::acc) swapped (y::xs)

    let rec sort arr =
        let result, swapped = bubblePass [] false arr
        if swapped then sort result else result

    sort arr

let rec insertionSort arr =
    let rec insert x sorted =
        match sorted with
        | [] -> [x]
        | y::ys when x <= y -> x::sorted
        | y::ys -> y :: insert x ys

    match arr with
    | [] -> []
    | first::rest -> insert first (insertionSort rest)