using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour {
    private string firstWord, secondWord;
    private string[] possibleCommands; // TODO: Will come from server

    public Command(string wordOne, string wordTwo)
    {
        if (checkCommand(wordOne) && checkCommand(wordTwo))
        {
            this.firstWord = wordOne;
            this.secondWord = wordTwo;
        }
    }

    public Command(string wordOne)
    {
        if (checkCommand(wordOne))
        {
            this.firstWord = wordOne;
        }
    }

    public void addWord(string word)
    {
        if (checkCommand(word))
        {
            if (firstWord == null)
                this.firstWord = word;
            else
                this.secondWord = word;
        }
    }


    // Use this for initialization
    void Start () {
       
    }
  
    bool checkCommand(string commandWord)
    {
        for (int i = 0; i < possibleCommands.Length; i++)
        {
            if (commandWord == possibleCommands[i])
                return true;
        }
        return false;
    } 
}
