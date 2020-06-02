﻿module Menu

open System
open PhonebookLogic

/// Implements user interface of Phonebook.
let menu =

    let rec menuRec (dataFromFile : string list list) (dataNotSavedToFileYet : string list list) =
        printfn "%s" "1 - exit"
        printfn "%s" "2 - add note (name and number)"
        printfn "%s" "3 - find number by name"
        printfn "%s" "4 - find name by number"
        printfn "%s" "5 - print the database"
        printfn "%s" "6 - save current data to the file"
        printfn "%s" "7 - read data from file"
        
        match Console.ReadLine() with
        | "1" -> 0
        | "2" ->
            printfn "%s" "Input name"
            printfn "%s" "Input number"
            menuRec dataFromFile ((addNote (Console.ReadLine()) (Console.ReadLine())) :: dataNotSavedToFileYet)
        | "3" -> 
            printfn "%s" "Input name"

            if dataFromFile <> [] then
                printfn "%s" "Input name" 
                let name = Console.ReadLine()
                match findNumberByName (dataFromFile @ dataNotSavedToFileYet) with
                | None -> 
                    printfn "%s" "Subscriber with that name does not exists"
                    menuRec dataFromFile dataNotSavedToFileYet

                | Some(number) -> 
                    printf "%A%s" name " " 
                    printfn "%A" number
                    menuRec dataFromFile dataNotSavedToFileYet
            else
                printfn "%s" "The database is empty"
                menuRec dataFromFile dataNotSavedToFileYet
        | "4" ->
            printfn "%s" "Input number"

            if not dataFromFile.IsEmpty then
                printfn "%s" "Input number"
                let number = Console.ReadLine()
                match findNameByNumber number (dataFromFile @ dataNotSavedToFileYet) with
                | None -> 
                    printfn "%s" "Subscriber with that number does not exists"
                | Some(name) -> 
                    printf "%A%s" name " "
                    printfn "%A" number
            else
                printfn "%s" "The database is empty"
            menuRec dataFromFile dataNotSavedToFileYet
        | "5" -> 
            printDatabase (dataFromFile @ dataNotSavedToFileYet)
            menuRec dataFromFile dataNotSavedToFileYet
        | "6" ->
            printfn "%s" "Input the name of the file"
            saveCurrentData (Console.ReadLine()) dataNotSavedToFileYet
            menuRec dataFromFile []
        | "7" ->
            printfn "%s" "Input the name of the file"
            menuRec (readInfoFromFile (Console.ReadLine())) dataNotSavedToFileYet
        | _ -> 
            printfn "%s" "Incorrect input"
            menuRec dataFromFile dataNotSavedToFileYet
        
    menuRec [] []