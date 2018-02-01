module DualSliderFS

open ReactRangeSlider
open Fable.Helpers.React.Props

module R = Fable.Helpers.React

type DualSliderFSProps<'a> =
    | Value1 of 'a
    | Value2 of 'a
    | OnSlider1Change of ('a -> unit)
    | OnSlider2Change of ('a -> unit)

let dualSliderFs props =
    let defaultProps : IHTMLProp list = [
        ReactRangeSliderProps.Min 0
        ReactRangeSliderProps.Max 100
        ReactRangeSliderProps.Orientation ReactRangeSliderProps.Horizontal
    ]
     
    let htmlAttrOption f x : IHTMLProp option = unbox f x |> Some
    let sliderProps f = List.choose f props |> (@) defaultProps
    
    let slider1Props =
        function
            | Value1 x -> htmlAttrOption ReactRangeSliderProps.Value x
            | OnSlider1Change x -> htmlAttrOption ReactRangeSliderProps.OnChange x
            | _ -> None
        |> sliderProps

    let slider2Props =
        function
            | Value2 x -> htmlAttrOption ReactRangeSliderProps.Value x
            | OnSlider2Change x -> htmlAttrOption ReactRangeSliderProps.OnChange x
            | _ -> None
        |> sliderProps

    R.div [] [
        ReactRangeSlider (slider1Props)
        ReactRangeSlider (slider2Props)
    ]