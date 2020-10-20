

open System
open System.IO


let frequentWords (text:string) (k:int) =
    let patternCounts =
        text
        |> Seq.windowed k
        |> Seq.map(fun c -> new string(c))
        |> Seq.countBy(id)
        |> Seq.sortByDescending(fun (s,c) -> c)
    let maxCount = patternCounts |> Seq.head |> snd
    patternCounts 
        |> Seq.filter(fun (s,c) -> c = maxCount)
        //|> PSeq.map(fun (s,c) -> s)

let fileName = @"D:\Git\Bioinformatics.fs\Ori.txt"
let ori = File.ReadAllText(fileName)

[3..9]
|> Seq.map(fun i -> i, frequentWords ori i)
|> Seq.iter(fun x -> printfn "%A" x)


