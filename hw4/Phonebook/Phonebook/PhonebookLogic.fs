﻿module PhonebookLogic

open System.IO
open Microsoft.FSharp.Core.Operators

(*Написать программу - телефонный справочник. Она должна уметь хранить имена и номера 
телефонов, в интерактивном режиме осуществлять следующие операции:
1. выйти
2. добавить запись (имя и телефон)
3. найти телефон по имени
4. найти имя по телефону
5. вывести всё текущее содержимое базы
6. сохранить текущие данные в файл
7. считать данные из файла
*)

/// Reads the notes from file.
let readInfoFromFile (fileName : string) store =
    let spaceIndex = Seq.findIndex(fun x -> x = ' ')
    
    let splitIntoListOfStrings (str : string) = [str.[..spaceIndex str]; str.[((spaceIndex str) + 1)..]]

    let parse arrStr =
        let rec parseRec arrStr store =
            match arrStr with
            | [] -> store
            | h :: t -> parseRec t (splitIntoListOfStrings h :: store)
        parseRec arrStr []

    try
        if store = [] then
            fileName 
            |> File.ReadAllLines |> Array.toList |> parse
        else store
    with | _ -> failwith "File was not opened or was not handled!"

/// Saves the current data to the file.
let saveCurrentData (fileName : string) data =
    let parse listListStr = 
        let rec parseRec listListStr listStr =
            match listListStr with
            | [] -> listStr
            | h :: t -> parseRec t ((List.head h + " " + List.last h) :: listStr)
        parseRec listListStr []
                
    try
        File.AppendAllLines(fileName, parse data)
    with | _ -> failwith "File was not opened!"

/// Add note (name and number).
let addNote name number = [name; number]

/// Prints the database.
let printDatabase store =
    let printList x =
        printf "%s" (List.head x)  
        printfn "%s" (List.last x)

    List.iter (fun x -> printList x) store

/// Finds a number by the name.
let findNumberByName name store =
    List.filter (fun x -> List.head x = name) store

/// Finds a name by the number.
let findNameByNumber number store =
    List.filter (fun x -> List.tail x = number) store