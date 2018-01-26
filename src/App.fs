module FableApp

open Elmish
open Elmish.HMR
open Elmish.React
open Fable.Core.JsInterop
open OpticsPlay
open Aether
open Aether.Operators

open Fable.Import

open Fable.Helpers.React.Props
module R = Fable.Helpers.React

type RCom = React.ComponentClass<obj>

let Slider = importDefault<RCom> "react-rangeslider"

importDefault "!style-loader!css-loader!react-rangeslider/lib/index.css"

let styles = importDefault<obj> "./App.scss"

let inline (~%) x = createObj x

type Model = RecordA

type Message =
    | SetEmptyValue
    | SetValue of string
    | SetSliderValue of int

let init() =
    { RecordA.B={ Value="testing" }
      sliderValue=50 }

let setavalue = Optic.set (RecordA.B_ >-> RecordB.Value_)
let update msg state =
    match msg with
    | SetValue newValue -> setavalue newValue state
    | SetEmptyValue -> setavalue "-----" state
    | SetSliderValue value -> { state with sliderValue = value }


let fill = Fable.Helpers.React.Props.Fill

let DualSlider = importDefault<RCom> "./components/DualSlider"

let view =
    (fun (model:RecordA) dispatch ->
        let rect x y w h = [ X x; Y y; !! ("width", w); !! ("height", h) ]
        R.div [ ClassName (!! styles?("app")) ] [
            R.input [
                DefaultValue model.B.Value
                OnChange (fun ev ->
                    let value = ev.target?value.ToString()
                    if value.Length > 0 then
                        dispatch (SetValue value)
                    else
                        dispatch (SetEmptyValue)
                )
            ]
            R.div [ Style [ FontSize 20 ] ] [
                R.ul [] [
                    R.li [] [ R.str model.B.Value ]
                ]
            ]
            R.div [] [
                R.div [ Style [ Float "left" ] ] [
                    R.svg [ !! ("width", "200px") ] [
                        R.rect ([ fill "rgb(200, 0, 0)" ] @ !! rect 10 10 model.sliderValue model.sliderValue) []
                        R.rect ([ fill "rgba(0, 0, 200, 0.5)" ] @ !! rect 30 30 model.sliderValue model.sliderValue) []
                    ]
                ]
                R.div [] [
                    R.str "fdsafdsafs"                    
                    R.from Slider
                        %["min" ==> 1
                          "max" ==> 100
                          "style" ==> "width: 50px; height: 200px"
                          "value" ==> model.sliderValue
                          "orientation" ==> "vertical"
                          "onChange" ==> (SetSliderValue >> dispatch)]
                        []
                    R.str "fdsafdsafs"                    
                ]
            ]
            R.from DualSlider %[
                "value1" ==> model.sliderValue
                "onSlider1Change" ==> (SetSliderValue >> dispatch)
            ] []
        ])

Program.mkSimple init update view
|> Program.withHMR
|> Program.withReact "elmish-app"
|> Program.run
