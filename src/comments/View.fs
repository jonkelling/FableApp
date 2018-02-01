module Comments.View

open Fable.Core
open Fable.Helpers.React.Props
open JsInterop
open Comments.Types

module R = Fable.Helpers.React

let private targetValue ev = !! ev?target?value

let render state dispatch =
    let renderWithoutUrl =
        R.div [] [ R.str "NO CURRENT URL" ]

    let renderWithUrl currentUrl =
        let sendClick _ =
            AddOrUpdateComment (currentUrl, state.currentCommentText)
        let renderComments comments =
            let mapped m mapper = List.map mapper m
            let result = mapped comments (fun comment ->
                R.div [] [
                    R.str <| match comment.text with | CommentText text -> text
                ])
            result
        let addButton text =
            R.button
                [ OnClick (sendClick >> dispatch) ]
                [ R.str text ]
        let input =
            R.input [
                Type "text"
                OnChange (targetValue >> SetCommentText >> dispatch)
            ]
        let buttonAndComments =
            match Map.tryFind currentUrl state.comments with
            | None ->
                [
                    addButton "Add First Comment!"
                    R.div [] [ R.str "No Comments yet!" ]
                ]
            | Some comments -> [ addButton "Add Comment" ] @ renderComments comments
        R.div [] ([input] @ buttonAndComments)       

    match state.currentUrl with
    | None -> renderWithoutUrl
    | Some currentUrl -> renderWithUrl currentUrl
