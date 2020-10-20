
open System

let patternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> Seq.map(fun c -> new string(c))
    |> Seq.filter(fun s -> s = pattern)
    |> Seq.length

let text = "ACAACTCTGCATACTATCGGGAACTATCCT"
let pattern = "ACTAT"
patternCount text pattern

let random = new Random(42)
let dictionary = ["A";"C";"G";"T"]
let baseNuclotides = [ for i in 0 .. 10000000 -> dictionary.[random.Next(4)]]
let nuclotides = String.Join("", baseNuclotides)

#time
let sequentialCounts = patternCount nuclotides pattern
sequentialCounts



#r "C:\\Users\\DIXON2019\\.nuget\\packages\\fsharp.collections.parallelseq\\1.1.2\\lib\\net45\\FSharp.Collections.ParallelSeq.dll"

open System
open FSharp.Collections.ParallelSeq

let parallelPatternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> PSeq.map(fun c -> new string(c))
    |> PSeq.filter(fun s -> s = pattern)
    |> PSeq.length

let parallelCounts = parallelPatternCount nuclotides pattern
parallelCounts