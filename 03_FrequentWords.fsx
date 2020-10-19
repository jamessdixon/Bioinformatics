
#r "/Users/jamesdixon/.nuget/packages/fsharp.collections.parallelseq/1.1.2/lib/net45/FSharp.Collections.ParallelSeq.dll"


open System
open System.IO
open FSharp.Collections.ParallelSeq

let frequentWords (text:string) (k:int) =
    let patternCounts =
        text
        |> Seq.windowed k
        |> PSeq.map(fun c -> new string(c))
        |> PSeq.countBy(id)
        |> Seq.sortByDescending(fun (s,c) -> c)
    let maxCount = patternCounts |> Seq.head |> snd
    patternCounts 
        |> PSeq.filter(fun (s,c) -> c = maxCount)
        //|> PSeq.map(fun (s,c) -> s)

let fileName = "Ori.txt"
let ori = File.ReadAllText(fileName)

[3..9]
|> Seq.map(fun i -> i, frequentWords ori i)
|> Seq.iter(fun x -> printfn "%A" x)


