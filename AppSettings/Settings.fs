module Settings
open System.IO
open System
open Newtonsoft.Json.Linq

let mutable private path = ""

let private data = JObject ()

let private locker = Object ()

let private save = async {
    lock locker (fun () -> 
        Threading.Thread.Sleep 6000
        File.WriteAllText (path, data.ToString ())
    )
}

let private propertyChanged p =
    Async.Start save 
    
data.PropertyChanged.Add propertyChanged

let initialize organization application = 
    let directory = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), organization, application)
    if  not (Directory.Exists directory) then Directory.CreateDirectory directory |> ignore
    path <- Path.Combine (directory, "settings.json")

let get () = data
  
let dispose () = Async.RunSynchronously save