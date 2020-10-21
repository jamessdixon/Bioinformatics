

open System
open System.IO

type DNA = A | C | T | G

let reverseCompliment (nuclotides: DNA seq) =
    nuclotides
    |> Seq.map(fun x -> match x with | A -> T | C -> G | T -> A | G -> C)
    |> Seq.rev

reverseCompliment [A;T;G]





