# FableApp

I've recently been hooked on finding a good way to use F# for web development. This is my attempt at learning Fable and its pros/cons.

## What's here so far

* FSharp, obviously.
* Webpack setup pulled near 100% from some Fable react-toolbox demo.
* Aether&mdash;just started looking at this and wanted to get a feel for it first hand.
* SCSS and CSS
* Using react-rangeslider, which is a third-party JS module _not_ written for F# or Fable. 
  * The CSS included with it is being imported from App.fs using !style!css! to turn off modules. Nice.
  * There is a way to write your own F# bindings, but I was exploring Fable's ability to dynamically reference JS for this one.
* **React!** Yep. I'm happy.
* Trying out the Elmish (or elm) architecture instead of Redux after studying their differences. Think I might buy into it eventually. Time will tell.


## Other F# Experimenting

### WebSharper

I briefly played around with WebSharper, and it's definitely worth another look. The ability to couple server/client code is awesome, and it was definitely the F# I was looking for. WebSharper fell short of ideal for me, however, when I couldn't easily find a way to use React or any other third party JS modules. Perhaps it's been improved, or I was just missing something. I wasn't a fan of the licensing either (though I totally support paying for good software).