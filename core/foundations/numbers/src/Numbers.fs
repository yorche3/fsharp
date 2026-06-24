module Numbers

let rec sumFirstNRec n =
    match n with
    | n when n <= 0 -> 0
    | _ -> n + sumFirstNRec (n - 1)

let rec factorialRec n =
    match n with
    | n when n <= 1 -> 1
    | _ -> n * factorialRec (n - 1)

let rec fibonacciRec n =
    match n with
    | n when n <= 0 -> 0
    | 1 -> 1
    | _ -> fibonacciRec (n - 1) + fibonacciRec (n - 2)

let rec greatestCommonDivisorRec a b =
    match b with
    | 0 -> a
    | _ -> greatestCommonDivisorRec b (a % b)

let leastCommonMultipleRec a b =
    let gcd = greatestCommonDivisorRec a b
    (a * b) / gcd

let rec sumFirstNHelp i acc =
    match i with
    | i when i <= 0 -> acc
    | _ -> sumFirstNHelp (i - 1) (acc + i)

let sumFirstNAcc n = sumFirstNHelp n 0

let rec factorialHelp i acc =
    match i with
    | i when i <= 1 -> acc
    | _ -> factorialHelp (i - 1) (acc * i)

let factorialAcc n = factorialHelp n 1

let rec fibonacciHelp i acc1 acc2 =
    match i with
    | i when i <= 0 -> acc1
    | _ -> fibonacciHelp (i - 1) acc2 (acc1 + acc2)

let fibonacciAcc n = fibonacciHelp n 0 1

let rec greatestCommonDivisorAcc a b =
    match b with
        | 0 -> a
        | _ -> greatestCommonDivisorAcc b (a % b)

let leastCommonMultipleAcc a b =
    let gcd = greatestCommonDivisorAcc a b
    (a * b) / gcd

let sumFirstNIte n =
    let rec loop i acc =
        match i with
        | i when i <= 0 -> acc
        | _ -> loop (i - 1) (acc + i)
    loop n 0

let factorialIte n =
    let rec loop i acc =
        match i with
        | i when i <= 1 -> acc
        | _ -> loop (i - 1) (acc * i)
    loop n 1

let fibonacciIte n =
    let rec loop i acc1 acc2 =
        match i with
        | i when i <= 0 -> acc1
        | _ -> loop (i - 1) acc2 (acc1 + acc2)
    loop n 0 1

let greatestCommonDivisorIte a b =
    let rec loop x y =
        match y with
        | y when y = 0 -> x
        | _ -> loop y (x % y)
    loop a b

let leastCommonMultipleIte a b =
    let gcd = greatestCommonDivisorIte a b
    (a * b) / gcd