using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/BookDialogs", order = 1)]
public class BookDialogs : ScriptableObject
{
    public string[,] book = new string[10, 10];
    public DialogStep[] dialogs;

    public string[,] GetBook()
    {
        for(int i = 0; i < dialogs.GetLength(0); i++)
        {
            var dialog = dialogs[i];
            
            book[dialog.chapter, dialog.paragraph] = dialog.message;
        }

        return book;
    }
}

[System.Serializable]
public class DialogStep
{
    public int chapter;
    public int paragraph;
    public string message;
}