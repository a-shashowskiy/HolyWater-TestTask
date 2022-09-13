using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveToJSON 
{ 
    public static void SaveData(GameSave gameSave)
    {
        string save = JsonUtility.ToJson(gameSave);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/saveData.json", save);
    }

    public static GameSave LoadData()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/saveData.json"))
        { 
            using StreamReader reader = new StreamReader(Application.persistentDataPath + "/saveData.json");
            string json =reader.ReadToEnd();

            GameSave data = JsonUtility.FromJson<GameSave>(json);
            return data;
        }
        else return null;
    }
}
[System.Serializable]
public class GameSave
{
    public bool vibro; // save vibro state 
    public bool sound; // save sound stateS
    public int cardsLeft; // Class must containe type of saved card or item to respawn it now this is total randome, and i don't see reasone save more 
}
