
open System
open System.IO
open System.Collections
open System.Collections.Generic

let hammingDistance (p:string) (q:string) =
    match p.Length = q.Length with
    | true ->
        Array.zip (p.ToCharArray()) (q.ToCharArray())
        |> Array.map(fun (p,q) -> q=p)
        |> Array.filter(fun x -> x=true)
        |> Array.length
        |> Some
    | false -> None
    
//let stringOne = "ACTG"
//let stringTwo = "AAAA"
//let currentDistance = hammingDistance stringOne stringTwo

let approximatePatternCount (text:string) (pattern:string) (d:int) =
    text
    |> Seq.windowed pattern.Length
    |> Seq.map(fun c -> new string(c))
    |> Seq.map(fun s -> hammingDistance pattern s)
    |> Seq.map(fun hd -> hd.Value)
    |> Seq.map(fun hd -> hd >= d)
    |> Seq.filter(fun x -> x = true)
    |> Seq.length

let fileName = @"/Users/jamesdixon/Projects/Bioinformatics/Vibrio_Cholerea.txt"
let text = File.ReadAllText(fileName)
let pattern = "ATGATCAAG"
let result = approximatePatternCount text pattern 9
result
