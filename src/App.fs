module FableApp

open Elmish
open Elmish.React
open Fable.Core.JsInterop
open OpticsPlay
// open Aether

type Model = RecordA

type Message = | SetValue of string

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

let update msg state =
    match msg with
    | SetValue newValue -> { state with B = { state.B with Value = newValue }}


open Fable.Helpers.React.Props
module R = Fable.Helpers.React

let view =
    lazyView2 (fun model dispatch ->
        R.div [] [
            R.input [ OnChange (fun ev ->
                let value = ev.target?value.ToString()
                dispatch (SetValue value)) ]
            R.div [ Style [ FontSize 20 ] ] [
                R.ul [] [
                    R.li [] [ R.str model.B.Value ]
                ]
            ]
        ])

Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.run
