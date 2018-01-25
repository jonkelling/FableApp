module FableApp

open Elmish
open Elmish.HMR
open Elmish.React
open Fable.Core.JsInterop
open OpticsPlay

type Model = RecordA

type Message =
    | SetEmptyValue
    | SetValue of string

let init() =
    { B={ Value="testing" } }
    // let canvas = Browser.document.getElementsByTagName_canvas().[0]
    // canvas.width <- 1000.
    // canvas.height <- 800.
    // let ctx = canvas.getContext_2d()
    // The (!^) operator checks and casts a value to an Erased Union type
    // See http://fable.io/docs/interacting.html#Erase-attribute
    // ctx.fillStyle <- !^"rgb(200,0,0)"
    // ctx.fillRect (10., 10., 55., 50.)
    // ctx.fillStyle <- !^"rgba(0, 0, 200, 0.5)"
    // ctx.fillRect (30., 30., 55., 50.)

let setavalue newvalue a = { a with B = { a.B with Value = newvalue } }
let update msg state =
    match msg with
    | SetValue newValue -> setavalue newValue state
    | SetEmptyValue -> setavalue "-----" state


open Fable.Helpers.React.Props
module R = Fable.Helpers.React

let view =
    (fun model dispatch ->
        R.div [] [
            R.input [ OnChange (fun ev ->
                let value = ev.target?value.ToString()
                if value.Length > 0 then
                    dispatch (SetValue value)
                else
                    dispatch (SetEmptyValue)
            ) ]
            R.div [ Style [ FontSize 20 ] ] [
                R.ul [] [
                    R.li [] [ R.str model.B.Value ]
                ]
            ]
        ])

Program.mkSimple init update view
|> Program.withHMR
|> Program.withReact "elmish-app"
|> Program.run
