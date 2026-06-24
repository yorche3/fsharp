module Tests

open System
open Xunit

[<Fact>]
let ``Addition Test`` () =
    Assert.Equal(5, Calculator.addition 2 3)

[<Fact>]
let ``Subtraction Test`` () =
    Assert.Equal(2, Calculator.subtraction 5 3)

[<Fact>]
let ``Multiplication Test`` () =
    Assert.Equal(12, Calculator.multiplication 3 4)

[<Fact>]
let ``Division Test`` () =
    Assert.Equal(3, Calculator.division 10 3)

[<Fact>]
let ``Modulus Test`` () =
    Assert.Equal(1, Calculator.modulus 10 3)
