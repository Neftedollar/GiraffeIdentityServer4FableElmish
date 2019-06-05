module Global
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import
open Fable.Import.React

let oidc = importAll<Oidc.IExports> "oidc-client"
Browser.console.log oidc
let settings = createEmpty<Oidc.UserManagerSettings>
settings.authority <- Some "http://localhost:5000"
settings.client_id <- Some "js"
settings.redirect_uri <- Some "http://localhost:8080/callback.html"
settings.response_type <- Some "code"
settings.scope <- Some "openid testApi"
settings.post_logout_redirect_uri  <- Some "http://localhost:8080/"
let mgr = oidc.UserManager.Create(settings)

type Page =
    | Home
    | Counter
    | About

let toPath page =
    match page with
    | About -> "/about"
    | Counter -> "/counter"
    | Home -> "/home"

type LinkProp =
    | To of string
    | Replace of bool
    interface IHTMLProp

type LinkAttributes =
    inherit HTMLAttributes
    abstract ``to`` : string with get, set
    abstract replace : bool with get, set

let private isModifiedEvent (e : MouseEvent) =
    e.metaKey || e.altKey || e.ctrlKey || e.shiftKey

let link (props : IHTMLProp list) children =
    let p : LinkAttributes = keyValueList CaseRules.LowerFirst props |> unbox
    let href = p.``to``
    let newProps : IHTMLProp list = [
        Href href
        OnClick (fun e ->
            match p.onClick with
            | Some handler ->
                handler.Invoke e
            | None ->
                if not e.defaultPrevented && e.button = 0.
                    && (Option.isNone p.target || p.target = Some "_self") && not (isModifiedEvent e) then
                    e.preventDefault()

                    if p.replace then Browser.history.replaceState ((), "", href)
                    else Browser.history.pushState ((), "", href)

                    let ev = Browser.document.createEvent_CustomEvent()
                    ev.initCustomEvent ("NavigatedEvent", true, true, obj())
                    Browser.window.dispatchEvent ev |> ignore
            )
    ]
    let restProps =
        props
        |> List.filter (fun i ->
            match i with
            | :? LinkProp -> false
            | :? HTMLAttr as a ->
                match a with
                | Href _ -> false
                | _ -> true
            | _ -> true)
    a (newProps @ restProps) children
