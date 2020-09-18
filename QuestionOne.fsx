
let patternCount (text:string) (pattern:string) =
    text 
    |> Seq.windowed pattern.Length 
    |> Seq.map(fun c -> new string(c))
    |> Seq.filter(fun s -> s = pattern)
    |> Seq.length

let text = "ACAACTCTGCATACTATCGGGAACTATCCT"
let pattern = "ACTAT"

patternCount text pattern