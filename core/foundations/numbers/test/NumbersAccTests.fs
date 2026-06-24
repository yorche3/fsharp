module NumbersAccTests

open System
open Xunit

[<Fact>]
let ``Sum of first N numbers using accumulator`` () =
    Assert.Equal(0, Numbers.sumFirstNAcc 0)
    Assert.Equal(6, Numbers.sumFirstNAcc 3)

[<Fact>]
let ``Factorial of a number using accumulator`` () =
    Assert.Equal(1, Numbers.factorialAcc 0)
    Assert.Equal(24, Numbers.factorialAcc 4)

[<Fact>]
let ``Fibonacci sequence using accumulator`` () =
    Assert.Equal(0, Numbers.fibonacciAcc 0)
    Assert.Equal(1, Numbers.fibonacciAcc 1)
    Assert.Equal(8, Numbers.fibonacciAcc 6)

[<Fact>]
let ``Greatest Common Divisor (GCD) using accumulator`` () =
    Assert.Equal(4, Numbers.greatestCommonDivisorAcc 12 8)
    Assert.Equal(1, Numbers.greatestCommonDivisorAcc 7 5)

[<Fact>]
let ``Least Common Multiple (LCM) using accumulator`` () =
    Assert.Equal(24, Numbers.leastCommonMultipleAcc 6 8)
    Assert.Equal(12, Numbers.leastCommonMultipleAcc 6 4)