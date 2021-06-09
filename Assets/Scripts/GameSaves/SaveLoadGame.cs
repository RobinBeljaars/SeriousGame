using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadGame
{
    public static List<Game> games = new List<Game>();

    public static void Save()
    {
        games.Add(Game.currentGame);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoadGame.games);
        file.Close();
    }

    public static void Load(){
        if(File.Exists(Application.persistentDataPath +"/savedGames.gd")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath +"/savedGames.gd", FileMode.Open);
            SaveLoadGame.games = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }
}
