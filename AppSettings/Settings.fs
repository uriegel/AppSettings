module Settings
open System.Runtime.InteropServices
open System.IO
open System
open Newtonsoft.Json.Linq

let mutable private subPath = ""
let mutable data = JObject()


let private path = Lazy<string>.Create <| fun () -> 
    if subPath = "" then failwith "AppSettings not initialized"
    else Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), subPath)

let initialize organization application = 
    subPath <- Path.Combine (organization, application)

let get () =
    data
