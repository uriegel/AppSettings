open System
open Newtonsoft.Json.Linq

[<EntryPoint>]
let main argv =
    Settings.initialize "URiegel" "Tester"
    let settings = Settings.get ()
    settings.["Affe"] <- JToken()
    9