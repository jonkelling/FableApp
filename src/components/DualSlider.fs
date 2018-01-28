module DualSlider

module R = Fable.Helpers.React

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

type RCom = React.ComponentClass<obj>

type DualSliderProps<'a when 'a : unmanaged> =
    | OnSlider1Change of ('a -> unit)
    | OnSlider2Change of ('a -> unit)
    | Value1 of 'a
    | Value2 of 'a

let DualSlider props children =
    R.from
        (importDefault<RCom> "./DualSlider")
        (keyValueList CaseRules.LowerFirst props)
        children

