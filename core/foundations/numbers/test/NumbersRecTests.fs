module NumbersRecTests

open System
open Xunit

[<Fact>]
let ``Sum of first N numbers using recursion`` () =
    Assert.Equal(0, Numbers.sumFirstNRec 0)
    Assert.Equal(6, Numbers.sumFirstNRec 3)

[<Fact>]
let ``Factorial of a number using recursion`` () =
    Assert.Equal(1, Numbers.factorialRec 0)
    Assert.Equal(24, Numbers.factorialRec 4)

[<Fact>]
let ``Fibonacci sequence using recursion`` () =
    Assert.Equal(0, Numbers.fibonacciRec 0)
    Assert.Equal(1, Numbers.fibonacciRec 1)
    Assert.Equal(8, Numbers.fibonacciRec 6)

[<Fact>]
let ``Greatest Common Divisor (GCD) using recursion`` () =
    Assert.Equal(4, Numbers.greatestCommonDivisorRec 12 8)
    Assert.Equal(1, Numbers.greatestCommonDivisorRec 7 5)

[<Fact>]
let ``Least Common Multiple (LCM) using recursion`` () =
    Assert.Equal(24, Numbers.leastCommonMultipleRec 6 8)
    Assert.Equal(12, Numbers.leastCommonMultipleRec 6 4)
