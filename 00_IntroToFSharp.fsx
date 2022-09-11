
let add1 x =
    x + 1

add 2 3


[1..10]
|> Seq.map(fun x -> x + 1)

[1..10]
|> Seq.map(add1)

[1..10]
|> Seq.map(fun x -> x + 1)
|> Seq.filter(fun x -> x % 2 = 0)
|> Seq.map(fun x -> x * x)
|> Seq.sum

[1..10]
|> Seq.windowed 2
