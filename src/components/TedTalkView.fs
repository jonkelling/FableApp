module TedTalkView

open Fable.Import
open TedTalkData

module R = Fable.Helpers.React

type RCom = React.ComponentClass<obj>

let (</>) a b = a [ b ]
let (</>>) a b = a [] [ b ]

let tedTalkView model =
    R.div [] [
        R.div </>> (R.str <| match model with
                             | None -> "No TED data"
                             | Some ted -> ted.description)
        R.div </>> R.str "fdsafdsa"                     
    ]