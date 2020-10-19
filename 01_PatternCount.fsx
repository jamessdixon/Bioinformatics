
//option + return

open System

let patternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> Seq.map(fun c -> new string(c))
    |> Seq.filter(fun s -> s = pattern)
    |> Seq.length

let text = "ACAACTCTGCATACTATCGGGAACTATCCT"
let pattern = "ACTAT"

let getRandomNuclotide () =
    let dictionary = ["A";"C";"G";"T"]
    let random = new Random()
    dictionary.[random.Next(4)]

let getRandomSequence (length:int) =
    let nuclotides = [ for i in 0 .. length -> getRandomNuclotide() ]
    String.Join("", nuclotides)

let largerText = getRandomSequence 10000000

#time
let functionalCounts = patternCount largerText pattern
functionalCounts

