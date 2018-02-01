module TedTalkData

open Fable.PowerPack
open Fetch

// open FSharp.Data.UnitSystems.SI.UnitSymbols
open FSharp.Data.UnitSystems.SI.UnitNames

type TedTalk = {
    comments : int
    description : string
    duration : int<second>
    event: string
    film_date: int<second>
    languages: int
    main_speaker: string
    name: string
    num_speaker: int
    published_date: int<second>
    ratings: obj
    related_talks: obj
    speaker_occupation: string
    tags: obj
    title: string
    url: string
    views: int
}

type TedTranscript = {
    transcript: string
    url: string
}

type Tedtalks = {
    ted: TedTalk list
    transcripts: TedTranscript list

} with
    static member GetTed = fun ted -> ted.ted

let data =
    promise {
        let url = "http://localhost:3000/api/tedtalks"
        let! response = fetchAs<Tedtalks> url []
        return response
    }