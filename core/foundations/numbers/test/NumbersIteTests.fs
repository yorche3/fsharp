module NumbersIteTests

open System
open Xunit

[<Fact>]
let ``Sum of first N numbers using iteration`` () =
    Assert.Equal(0, Numbers.sumFirstNIte 0)
    Assert.Equal(6, Numbers.sumFirstNIte 3)

[<Fact>]
let ``Factorial of a number using iteration`` () =
    Assert.Equal(1, Numbers.factorialIte 0)
    Assert.Equal(24, Numbers.factorialIte 4)

[<Fact>]
let ``Fibonacci sequence using iteration`` () =
    Assert.Equal(0, Numbers.fibonacciIte 0)
    Assert.Equal(1, Numbers.fibonacciIte 1)
    Assert.Equal(8, Numbers.fibonacciIte 6)

[<Fact>]
let ``Greatest Common Divisor (GCD) using iteration`` () =
    Assert.Equal(4, Numbers.greatestCommonDivisorIte 12 8)
    Assert.Equal(1, Numbers.greatestCommonDivisorIte 7 5)

[<Fact>]
let ``Least Common Multiple (LCM) using iteration`` () =
    Assert.Equal(24, Numbers.leastCommonMultipleIte 6 8)
    Assert.Equal(12, Numbers.leastCommonMultipleIte 6 4)