

open System
open System.IO

let reverseCompliment (text: string) =  
    let charArray =
        text.ToLowerInvariant().ToCharArray()
        |> Seq.map(fun x -> match x with | 'a' -> 't' | 'c' -> 'g' | 't' -> 'a' | 'g' -> 'c' | _ -> ' ')
        |> Seq.rev
        |> Seq.toArray
    new string(charArray)

reverseCompliment "ATG"

let frequentWords (text:string) (k:int) =
    let patternCounts =
        text
        |> Seq.windowed k
        |> Seq.map(fun c -> new string(c))
        |> Seq.countBy(id)
        |> Seq.sortByDescending(fun (s,c) -> c)
    let maxCount = patternCounts |> Seq.head |> snd
    let topPatterns =
        patternCounts 
        |> Seq.filter(fun (s,c) -> c = maxCount)
    topPatterns
    |> Seq.map(fun (s,c) -> s,c, reverseCompliment s)
    |> Seq.map(fun (s,c,r) -> s,c, patternCounts |> Seq.find(fun (s,c) -> s = r))

let fileName = "Ori.txt"
let ori = File.ReadAllText(fileName)

[3..9]
|> Seq.map(fun i -> i, frequentWords ori i)
|> Seq.iter(fun x -> printfn "%A" x)

//42
//19
//16,16
//16
//9
//7
//6,4


