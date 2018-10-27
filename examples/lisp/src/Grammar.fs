// This code was automatically generated from a grammar definition by Fohm.


namespace Lisp.Grammar

open Lisp.Syntax


open Fable.Core
open Fable.Core.JsInterop

module Lisp =
  [<Import("makeParser", from="./fohm-runtime.js")>]
  let private makeParser (source: string, visitor: obj): obj = jsNative

  type Offset = 
    { line: int; column: int }

  type OffsetRecord<'a> =
    { start: 'a; ``end``: 'a }

  type Position = 
    {
      offset: unit -> OffsetRecord<int>
      position: unit -> OffsetRecord<Offset>
      sourceSlice: string
    }

  type Meta = 
    { source: Position; children: Position list }

  let private visitor = 
    createObj [
      "Program_alt0" ==> fun (meta:Meta) forms ->
         List(forms) 
                
      "Expr_alt0" ==> fun (meta:Meta) _0 _1 id value _4 ->
         Define(id, value) 
                
      "Expr_alt1" ==> fun (meta:Meta) _0 _1 _2 ids _4 body _6 ->
         Lambda(ids, body) 
                
      "Expr_alt2" ==> fun (meta:Meta) _0 head args _3 ->
         Call(head, args) 
                
      "Expr_alt3" ==> fun (meta:Meta) id ->
         Symbol(id) 
                
      "Expr_alt4" ==> fun (meta:Meta) n ->
         Number(n) 
                
    ]

  let private primParser: obj  =
    makeParser(
      """
      Lisp {
        Program =
          | Expr* -- alt0
                
        
        Expr =
          | "(" "define" name Expr ")" -- alt0
          | "(" "lambda" "[" name* "]" Expr* ")" -- alt1
          | "(" Expr Expr* ")" -- alt2
          | name -- alt3
          | number -- alt4
                
        
        name =
          | letter alnum* -- alt0
                
        
        number =
          | digit+ -- alt0
                
      }
        
      """, 
      visitor
    )

  type ParseResult<'T> =
    | Ok of 'T
    | Error of string

  let parse (source: string): ParseResult<LispExpr> = 
    let (success, value) = !!(!!primParser)(source)
    if success then Ok(!!value)
    else Error(!!value)
  
