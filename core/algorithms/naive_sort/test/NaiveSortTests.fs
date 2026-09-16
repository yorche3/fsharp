module NaiveSortTests

open Xunit

// Casos de prueba de la especificación 05_Naive_Sort.md
let standardInput = [ 5; 2; 9; 1; 5; 6 ]
let standardOutput = [ 1; 2; 5; 5; 6; 9 ]

let sortedInput = [ 1; 2; 3; 4; 5 ]
let sortedOutput = [ 1; 2; 3; 4; 5 ]

let reverseInput = [ 5; 4; 3; 2; 1 ]
let reverseOutput = [ 1; 2; 3; 4; 5 ]

let identicalInput = [ 7; 7; 7; 7 ]
let identicalOutput = [ 7; 7; 7; 7 ]

let negativeInput = [ 3; -1; 4; -5; 0 ]
let negativeOutput = [ -5; -1; 0; 3; 4 ]

let singleInput = [ 42 ]
let singleOutput = [ 42 ]

let emptyInput: int list = []
let emptyOutput: int list = []

let cases =
    [ ("an unsorted array", standardInput, standardOutput)
      ("an already sorted array", sortedInput, sortedOutput)
      ("a reverse ordered array", reverseInput, reverseOutput)
      ("an array of identical elements", identicalInput, identicalOutput)
      ("an array with negative numbers", negativeInput, negativeOutput)
      ("a single element array", singleInput, singleOutput)
      ("an empty array", emptyInput, emptyOutput) ]

// Las listas de F# son inmutables: no hace falta copiar los fixtures porque
// ninguna función puede mutarlos entre casos.
let assertSortsAllCases (sortFunction: int list -> int list) (algorithm: string) =
    for (description, input, expected) in cases do
        Assert.True((sortFunction input = expected), $"{algorithm} should sort {description}")

[<Fact>]
let ``Selection sort`` () =
    assertSortsAllCases NaiveSort.selectionSort "selection_sort"

[<Fact>]
let ``Bubble sort`` () =
    assertSortsAllCases NaiveSort.bubbleSort "bubble_sort"

[<Fact>]
let ``Insertion sort`` () =
    assertSortsAllCases NaiveSort.insertionSort "insertion_sort"
