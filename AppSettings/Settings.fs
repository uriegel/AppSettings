module Settings
open System.Runtime.InteropServices
open System.IO
open System
open Newtonsoft.Json.Linq

let mutable private path = ""

let private data = JObject ()

let private locker = Object ()

let private propertyChanged p =
    async {
        lock locker (fun () -> 
            File.WriteAllText (path, data.ToString ())
        )
    } |> Async.Start
    
data.PropertyChanged.Add propertyChanged

let initialize organization application = 
    let directory = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), organization, application)
    if  not (Directory.Exists directory) then Directory.CreateDirectory directory |> ignore
    path <- Path.Combine (directory, "settings.json")

let get () = data
  