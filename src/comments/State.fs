module Comments.State

open Comments.Types
open Elmish
open FSharp.Data.UnitSystems.SI.UnitNames

let init() =
    {
        currentCommentText = ""
        currentUrl = None
        comments = Map.empty<Url, Comment list>
    } , Cmd.none

let update msg state =
    let newState =
        match msg with
        | Comments.Types.Msg.SetCommentText value -> { state with currentCommentText = value }
        | Comments.Types.Msg.AddOrUpdateComment (url, text) ->
            let comment = { url = url; time = 0<second>; text = CommentText text }
            let newComments =
                match Map.tryFind url state.comments with
                | None -> [ comment ]
                | Some comments -> [ comment ] @ comments
            { state with comments = (Map.add url newComments state.comments) }
    newState, Cmd.none