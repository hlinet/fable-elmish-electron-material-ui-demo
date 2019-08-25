﻿module Badges

open System
open Elmish.React
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.MaterialUI.MaterialDesignIcons
open Fable.MaterialUI.Icons
open Feliz
open Feliz.MaterialUI


type Model =
  { Count: int }

type Msg =
  | Increment
  | Decrement

let init () =
  { Count = 2 }

let update msg m =
  match msg with
  | Increment -> { m with Count = m.Count + 1 }
  | Decrement -> { m with Count = m.Count - 1 }


// Domain/Elmish above, view below


let private useStyles = Styles.makeStyles(fun theme ->
  {|
    description = asClassName [
      style.marginBottom (theme.spacing.unit * 3)
    ]
    margin = asClassName [
      style.marginRight (theme.spacing.unit * 2)
    ]
  |}
)

let BadgesPage = FunctionComponent.Of((fun (model, dispatch) ->
  let c = useStyles ()
  Html.div [
    prop.children [

      Mui.typography [
        prop.className c.description
        typography.paragraph true
        typography.children "The badge becomes invisible when count = 0."
      ]

      Mui.iconButton [
        prop.className c.margin
        prop.children [
          Mui.badge [
            badge.color.secondary
            badge.badgeContent model.Count
            badge.children [ notificationsIcon [] ]
          ]
        ]
      ]

      Mui.iconButton [
        prop.onClick (fun _ -> dispatch Decrement)
        iconButton.disabled (model.Count <= 0)
        iconButton.children [ minusIcon [] ]
      ]

      Mui.iconButton [
        prop.onClick (fun _ -> dispatch Increment)
        iconButton.children [ plusIcon [] ]
      ]
    ]
  ]
), "BadgesPage", memoEqualsButFunctions)
