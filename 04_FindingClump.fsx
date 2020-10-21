

open System
open System.IO

let patternCount (nuclotides: string) (pattern: string) =
    let patternSize = pattern.Length
    nuclotides
    |> Seq.windowed patternSize
    |> Seq.map(fun c -> new string(c))
    |> Seq.filter(fun c ->  c = pattern)
    |> Seq.length

let getClump (genome:string) idx oriSize =
    if idx%oriSize=0 then 
        if idx+oriSize > genome.Length then
            Some(genome.Substring(idx,genome.Length-idx))
        else
            Some(genome.Substring(idx,oriSize)) 
    else None

let clumpFinding (clumps: string seq) (pattern: string) =
    clumps
    |> Seq.mapi(fun idx c ->  idx, (patternCount c pattern))
    |> Seq.sortByDescending(fun (i,c) -> c)

let getClumps (genome:string) (oriSize: int)=
    genome
    |> Seq.mapi(fun idx c -> getClump genome idx oriSize)
    |> Seq.filter(fun v -> v.IsSome)
    |> Seq.map(fun v -> v.Value)
    |> Seq.toArray

//let fileName = "Vibrio_Cholerea.txt"
let fileName = @"D:\Git\Bioinformatics.fs\Vibrio_Cholerea.txt"
let genome = File.ReadAllText(fileName)
let clumpSize = 500
let clumps = getClumps genome clumpSize
let pattern = "ATGATCAAG"
let foundClumps = clumpFinding clumps pattern

let maxFoundInClump = foundClumps |> Seq.head |> snd
let maxClumps = foundClumps |> Seq.filter(fun x -> snd x = maxFoundInClump )
let maxClumpsLength = maxClumps |> Seq.length
let clumpsLength = clumps |> Seq.length

maxClumpsLength
clumpsLength
genome.Length