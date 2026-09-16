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
