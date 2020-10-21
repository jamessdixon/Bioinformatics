
#r "C:\\Users\\DIXON2019\\.nuget\\packages\\fsharp.collections.parallelseq\\1.1.2\\lib\\net45\\FSharp.Collections.ParallelSeq.dll"
//#r "/Users/jamesdixon/.nuget/packages/fsharp.collections.parallelseq/1.1.2/lib/net45/FSharp.Collections.ParallelSeq.dll"

open System
open FSharp.Collections.ParallelSeq

let random = Random(42)
let dictionary = ["A";"C";"G";"T"]
let baseNuclotides = [ for i in 0 .. 100000000 -> dictionary.[random.Next(4)]]
let nuclotides = String.Join("", baseNuclotides)

let patternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> PSeq.map(fun c -> new string(c))
    |> PSeq.filter(fun s -> s = pattern)
    |> PSeq.length

#time
let pattern = "ACTAT"
let functionalCounts = patternCount nuclotides pattern
functionalCounts

//Real: 00:00:12.710, CPU: 00:00:12.703, GC gen0: 1722, gen1: 2, gen2: 1
//Real: 00:00:06.951, CPU: 00:00:12.818, GC gen0: 1728, gen1: 1, gen2: 0
