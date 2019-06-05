module App.State

open Elmish
open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Fable.PowerPack
open Global
open Types



let pageParser: Parser<Page->Page,Page> =
    oneOf [
        map About (s "about")
        map Counter (s "counter")
        map Home (oneOf [ s "home"; s "index.html"] )
    ]

let urlUpdate (result : Page option) model =
    match result with
    | None ->
        console.error("Error parsing url")
        model, Navigation.newUrl  (toPath model.CurrentPage)
    | Some page ->
        { model with CurrentPage = page }, []

let init result =
    let (counter, counterCmd) = Counter.State.init()
    let (home, homeCmd) = Home.State.init()
    let (model, cmd) =
        urlUpdate result
          { CurrentPage = Home
            Counter = counter
            User = None
            Home = home
            Claims = "Anauthorized" }
    let getUserCmd =
        Cmd.ofPromise (fun _ -> console.log("trying to get user") ; mgr.getUser()) () (fun u -> GotUser u) (fun e -> console.error e; GotUser None)
    model, Cmd.batch [ cmd
                       getUserCmd
                       Cmd.map CounterMsg counterCmd
                       Cmd.map HomeMsg homeCmd ]

let update msg model =
    match msg with
    | CounterMsg msg ->
        let (counter, counterCmd) = Counter.State.update msg model.Counter
        { model with Counter = counter }, Cmd.map CounterMsg counterCmd
    | HomeMsg msg ->
        let (home, homeCmd) = Home.State.update msg model.Home
        { model with Home = home }, Cmd.map HomeMsg homeCmd
    | GotUser u ->
        {model with User = u },
         if u.IsSome then
             let headers = Fetch.requestHeaders [ Fetch.Fetch_types.Authorization (sprintf "Bearer %s" u.Value.access_token)  ]
             Cmd.ofPromise (fun _ -> Fetch.fetch  "http://localhost:5001/identity"  [headers] |> Promise.bind (fun x -> x.text()))
                           ()
                           (fun json -> GotClaims <| sprintf " Claims: %s \n User: %O" json u.Value)
                           (fun _ -> GotClaims "none")
         else
             Cmd.Empty
    | GotClaims txt ->
          { model with Claims = txt }, []
