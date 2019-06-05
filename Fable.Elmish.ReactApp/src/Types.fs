module App.Types

open Global
open Oidc

type Msg =
    | CounterMsg of Counter.Types.Msg
    | HomeMsg of Home.Types.Msg
    | GotUser of Oidc.User option
    | GotClaims of string

type Model =
    { CurrentPage: Page
      User : User option
      Counter: Counter.Types.Model
      Home: Home.Types.Model
      Claims : string }
