open System
open Newtonsoft.Json.Linq

[<EntryPoint>]
let main argv =
    Settings.initialize "URiegel" "Tester"
    let settings = Settings.get ()

    let name = settings.["name"]
    settings.["name"] <- JValue "Uwe Riegel"
    let name2 = string settings.["Affe"]
    settings.["size"] <- JValue 3456
    let json = settings.ToString()

    let settings2 = Settings.get ()
    let json2 = settings2.ToString()
    settings.["values"] <- JArray [|"Uwe Riegel"; "Miles Davis" |]
    let values = settings.["values"].Values ()

    let line = Console.ReadLine ()
    Settings.dispose ()
    9