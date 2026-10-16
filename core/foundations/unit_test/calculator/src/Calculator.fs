module Calculator

let addition a b = a + b
    
let subtraction a b = a - b
    
let multiplication a b =
    let rec loop acc i =
        if i = 0 then acc
        else loop (addition acc a) (subtraction i 1)
    loop 0 b

let division a b =
    let rec loop quotient remaining =
        if remaining < b then quotient
        else loop (addition quotient 1) (subtraction remaining b)
    loop 0 a

let modulus a b =
    let quotient = division a b
    subtraction a (multiplication quotient b)