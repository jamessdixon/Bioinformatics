
#r "/Users/jamesdixon/.nuget/packages/fsharp.collections.parallelseq/1.1.2/lib/net45/FSharp.Collections.ParallelSeq.dll"

open System
open FSharp.Collections.ParallelSeq

let patternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> PSeq.map(fun c -> new string(c))
    |> PSeq.filter(fun s -> s = pattern)
    |> PSeq.length


let getRandomNuclotide () =
    let dictionary = ["A";"C";"G";"T"]
    let random = Random()
    dictionary.[random.Next(4)]

let getRandomSequence (length:int) =
    let nuclotides = [ for i in 0 .. length -> getRandomNuclotide() ]
    String.Join("", nuclotides)

let largerText = getRandomSequence 10000000

let pattern = "ACTAT"
let functionalCounts = patternCount largerText pattern
functionalCounts

