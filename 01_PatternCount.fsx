
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
