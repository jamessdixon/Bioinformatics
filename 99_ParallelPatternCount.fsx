
#r "nuget: FSharp.Collections.ParallelSeq"

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

//Real: 00:00:06.946, CPU: 00:00:07.194, GC gen0: 1721, gen1: 3, gen2: 0
//Real: 00:00:05.902, CPU: 00:00:11.017, GC gen0: 1732, gen1: 0, gen2: 0

