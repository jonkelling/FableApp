module OpticsPlay

type RecordB =
    { Value: string }

    static member Value_ =
        (fun b -> b.Value),
        (fun value b -> { b with Value = value })

type RecordA =
    { B : RecordB
      sliderValue: int }

    static member B_ =
        (fun a -> a.B),
        (fun b a -> { a with B = b })

type RecordC =
    { B : RecordB option }

    static member B_ =
        (fun c -> c.B),
        (fun b (c:RecordC) -> { c with B = b })

let fdsa b a = { a with B = b }

let getB a = a.B
let getValue b = b.Value
let get = getB >> getValue

let setB b a = { a with B = b }
let setValue value b = { b with Value = value }
let set value a = setB (setValue value (getB a)) a

let f1 value a = setValue value (getB a)

let ff f1 f2 x = f2 (f1 x) x

let f2 value = (getB >> setValue value)
let f3 value a = setB ((getB >> setValue value) a) a