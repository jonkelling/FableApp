module Tedtalks

open Fable.PowerPack
open Fetch

type Tedtalks = {
    ted: obj
    transcripts: obj
}

let data =
    promise {
        let url = "http://localhost:3000/api/tedtalks"
        let! response = fetchAs<Tedtalks> url []
        return response
    }