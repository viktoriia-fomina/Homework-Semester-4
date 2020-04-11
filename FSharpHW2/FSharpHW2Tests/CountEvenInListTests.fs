module FSharpHW2Tests

open NUnit.Framework
open FsUnit
open FsCheck
open CountEvenInList

[<Test>]
let ``CountEvenInListFold for the list [1; 2; 4; 5; -1; 7] should return Some(2)`` () =
    countEvenInListFold [1; 2; 4; 5; -1; 7] |> should equal (Some 2)

[<Test>]
let ``CountEvenInListFilter for the list [1; 2; 4; 5; -1; 7] should return Some(2)`` () =
    countEvenInListFilter [1; 2; 4; 5; -1; 7] |> should equal (Some 2)
