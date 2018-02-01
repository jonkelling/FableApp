module Comments.Types

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Aether

type Url = Url of string
type CommentTime = int<second>
type CommentText = CommentText of string

type Comment = {
    url : Url
    time : int<second>
    text : CommentText
} with
    member this.Url =
        (fun _ -> this.url),
        (fun value -> { this with url = value })

type Msg =
    | SetCommentText of string
    | AddOrUpdateComment of Url * string

type State = {
    currentCommentText : string
    currentUrl : Url option
    comments: Map<Url, Comment list>
}