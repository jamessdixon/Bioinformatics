
#r "/Users/jamesdixon/.nuget/packages/fsharp.collections.parallelseq/1.1.2/lib/net45/FSharp.Collections.ParallelSeq.dll"

open System
open System.IO
open FSharp.Collections.ParallelSeq

type DNA = A | C | T | G

let reverseCompliment (nuclotides: DNA seq) =
    nuclotides
    |> PSeq.map(fun x -> match x with | A -> T | C -> G | T -> A | G -> C)
    |> Seq.rev

let fileName = "Ori.txt"
let inputString = File.ReadAllText(fileName)
let ori =
    inputString.ToLowerInvariant().ToCharArray()
    |> Array.toSeq
    |> PSeq.map(fun c -> match c with 'a' -> Some A | 'c' -> Some C | 't' -> Some T | 'g' -> Some G | _ -> None)
    |> PSeq.filter(fun d -> d.IsSome)
    |> PSeq.map(fun d -> d.Value)

ori 
reverseCompliment ori

