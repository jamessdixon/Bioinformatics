
#r "C:\\Users\\DIXON2019\\.nuget\\packages\\fsharp.collections.parallelseq\\1.1.2\\lib\\net45\\FSharp.Collections.ParallelSeq.dll"

open System
open System.IO
open FSharp.Collections.ParallelSeq

type DNA = A | C | T | G

let reverseCompliment (nuclotides: DNA seq) =
    nuclotides
    |> Seq.map(fun x -> match x with | A -> T | C -> G | T -> A | G -> C)
    |> Seq.rev

reverseCompliment [A;T;G;G;A;T;C;A;A;G]


let fileName = @"D:\Git\Bioinformatics.fs\Ori.txt"
let inputString = File.ReadAllText(fileName)
let ori =
    inputString.ToLowerInvariant().ToCharArray()
    |> Array.toSeq
    |> PSeq.map(fun c -> match c with 'a' -> Some A | 'c' -> Some C | 't' -> Some T | 'g' -> Some G | _ -> None)
    |> PSeq.filter(fun d -> d.IsSome)
    |> PSeq.map(fun d -> d.Value)


let frequentWords (nuclotides: DNA seq) (k:int) =
    let forwardStrand = nuclotides
    let reverseStrand = reverseCompliment nuclotides
    let totalStand = Seq.append forwardStrand reverseStrand

    let patternCounts =
        totalStand
        |> Seq.windowed k
        |> PSeq.map(fun c -> new string(c))
        |> PSeq.countBy(id)
        |> Seq.sortByDescending(fun (s,c) -> c)
    let maxCount = patternCounts |> Seq.head |> snd
    patternCounts 
        |> PSeq.filter(fun (s,c) -> c = maxCount)
        //|> PSeq.map(fun (s,c) -> s)

[3..9]
|> Seq.map(fun i -> i, frequentWords ori i)
|> Seq.iter(fun x -> printfn "%A" x)


