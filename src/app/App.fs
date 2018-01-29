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
open ReactRangeSlider
open System
open DualSlider
open Tedtalks
open Fable.PowerPack

module R = Fable.Helpers.React

type RCom = React.ComponentClass<obj>

let styles = importDefault<obj> "./App.scss"

type Model = {
    Data1: RecordA
    Ted: Tedtalks option
}

type Message =
    | SetEmptyValue
    | SetValue of string
    | SetSliderValue of int
    | TedtalkData of Tedtalks.Tedtalks option

let init() =
    {
        Data1 =
            {
                B={ Value="testing" }
                sliderValue=50
            }
        Ted = None
    }

let setavalue = Optic.set (RecordA.B_ >-> RecordB.Value_)
let update msg state =
    match msg with
    | SetValue newValue -> { state with Data1 = setavalue newValue state.Data1 }
    | SetEmptyValue -> { state with Data1 = setavalue "-----" state.Data1 }
    | SetSliderValue value -> { state with Data1 = { state.Data1 with sliderValue = value } }
    | TedtalkData value -> { state with Ted = value }


let fill = Fable.Helpers.React.Props.Fill


let view =
    (fun model dispatch ->
        let rect x y w h = [ X x; Y y; !! ("width", w); !! ("height", h) ]
        let (sliderProps:IHTMLProp list) = [
            //   "style" ==> "width: 50px; height: 200px"
            ReactRangeSliderProps.Min 0
            ReactRangeSliderProps.Max <|
                match model.Ted with
                | Some data -> data.ted.Length - 1
                | None -> 100
            ReactRangeSliderProps.Value model.Data1.sliderValue
            ReactRangeSliderProps.Orientation ReactRangeSliderProps.Vertical
            ReactRangeSliderProps.OnChange (SetSliderValue >> dispatch |> Some)
        ]
        R.div [ ClassName (!! styles?("app")) ] [
            R.input [
                DefaultValue model.Data1.B.Value
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
                    R.li [] [ R.str model.Data1.B.Value ]
                ]
            ]
            R.div [] [
                R.div [ Style [ Float "left" ] ] [
                    R.svg [ !! ("width", "200px") ] [
                        R.rect ([ fill "rgb(200, 0, 0)" ] @ !! rect 10 10 model.Data1.sliderValue model.Data1.sliderValue) []
                        R.rect ([ fill "rgba(0, 0, 200, 0.5)" ] @ !! rect 30 30 model.Data1.sliderValue model.Data1.sliderValue) []
                    ]
                ]
                R.div [] [
                    ReactRangeSlider
                        sliderProps
                        []
                    R.str "fdsafdsafs"
                ]
            ]
            R.div [] [
                DualSlider [
                    Value1 model.Data1.sliderValue
                    Value2 (101 - model.Data1.sliderValue)
                    OnSlider1Change (SetSliderValue >> dispatch)
                    OnSlider2Change (
                        ((-) 101) >> Convert.ToDouble >>
                        ((*) 0.5) >> Convert.ToInt32 >> 
                        SetSliderValue >> dispatch)
                ] []
                ReactRangeSlider
                    (sliderProps @ [ ReactRangeSliderProps.Orientation ReactRangeSliderProps.Horizontal ])
                    []
                ReactRangeSlider
                    (sliderProps @ [
                        Style [ Width "50px"; Height "200px" ]
                        ReactRangeSliderProps.Value (101 - model.Data1.sliderValue)
                        ReactRangeSliderProps.Orientation ReactRangeSliderProps.Horizontal
                        ReactRangeSliderProps.OnChange (
                            ((-) 101) >> Convert.ToDouble >>
                            ((*) 0.5) >> Convert.ToInt32 >>
                            SetSliderValue >> dispatch |> Some)
                    ])
                    []
            ]
            R.div [] [
                R.button [
                        OnClick (fun _ ->
                            dispatch <| TedtalkData None
                            Tedtalks.data
                            |> Promise.map (Some >> TedtalkData >> dispatch)
                            |> ignore)
                    ] [
                        R.str "Load Data"
                    ]
            ]
            R.div [] [
                R.str
                <| match model.Ted with
                    | None -> "No TED data"
                    | Some ted -> (List.item (model.Data1.sliderValue) (ted.ted)).description
            ]
        ])

Program.mkSimple init update view
|> Program.withHMR
|> Program.withReact "elmish-app"
|> Program.run
