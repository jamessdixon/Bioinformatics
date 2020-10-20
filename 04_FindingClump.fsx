
#r "C:\\Users\\DIXON2019\\.nuget\\packages\\fsharp.collections.parallelseq\\1.1.2\\lib\\net45\\FSharp.Collections.ParallelSeq.dll"

open System
open System.IO
open FSharp.Collections.ParallelSeq

let patternCount (nuclotides: string) (pattern: string) =
    let patternSize = pattern.Length
    nuclotides
    |> Seq.windowed patternSize
    |> PSeq.map(fun c -> new string(c))
    |> PSeq.filter(fun c ->  c = pattern)
    |> PSeq.length

let getClump (genome:string) idx oriSize =
    if idx%oriSize=0 then 
        if idx+oriSize > genome.Length then
            Some(genome.Substring(idx,genome.Length-idx))
        else
            Some(genome.Substring(idx,oriSize)) 
    else None

let clumpFinding (clumps: string seq) (pattern: string) =
    clumps
    |> PSeq.mapi(fun idx c ->  idx, (patternCount c pattern))
    |> Seq.sortByDescending(fun (i,c) -> c)

let getClumps (genome:string) (oriSize: int)=
    genome
    |> PSeq.mapi(fun idx c -> getClump genome idx oriSize)
    |> PSeq.filter(fun v -> v.IsSome)
    |> PSeq.map(fun v -> v.Value)
    |> Seq.toArray


let fileName = "Vibrio_Cholerea.txt"
let genome = File.ReadAllText(fileName)
let oriSize = 500
let clumps = getClumps genome oriSize
let pattern = "ATGATCAAG"
let foundClumps = clumpFinding clumps pattern

let maxFoundInClump = foundClumps |> Seq.head |> snd
let maxClumps = foundClumps |> Seq.filter(fun x -> snd x = maxFoundInClump )
let maxClumpsLength = maxClumps |> Seq.length
let clumpsLength = clumps |> Seq.length

maxClumpsLength
clumpsLength
genome.Length