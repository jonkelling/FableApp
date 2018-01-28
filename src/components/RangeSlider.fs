
module ReactRangeSlider

open Elmish
open Fable.Core.JsInterop
open Fable.Core
open Fable.Import
open Fable.Helpers.React.Props

importDefault "!style-loader!css-loader!react-rangeslider/lib/index.css"

module public ReactRangeSliderProps =
    [<StringEnum>]
    type Orientation = 
        | [<CompiledName("horizontal")>] Horizontal
        | [<CompiledName("vertical")>] Vertical

    type ReactRangeSliderNumericAttr<'a when 'a : unmanaged> =
        | Min of 'a
        | Max of 'a
        | Step of 'a
        | Value of 'a
        | Format of ('a -> string) option
        | OnChange of ('a -> unit) option
        interface IHTMLProp

    type ReactRangeSliderOtherAttr =
        | Orientation of Orientation
        | Tooltip of bool
        | Reverse of bool
        | Labels of obj
        | HandleLabel of string option
        | OnChangeStart of (unit -> unit) option
        | OnChangeComplete of (unit -> unit) option
        interface IHTMLProp

type RCom = React.ComponentClass<obj>

let ReactRangeSlider props children =
    let f1 =
        Fable.Helpers.React.from
            (importDefault<RCom> "react-rangeslider")
    let f2 =
        f1
            (keyValueList CaseRules.LowerFirst props)
            children
    f2