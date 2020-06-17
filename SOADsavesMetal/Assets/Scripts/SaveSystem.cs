using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    static string path = Application.persistentDataPath + "/SOAD.save";

    public static SaveData SaveGame() //will assume player is not new
    {
        Debug.Log("Saved current game.");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(path);

        SaveData data = new SaveData();

        formatter.Serialize(stream, data);
        stream.Close();
        return data;
    }

    public static SaveData LoadGame() //do this at start of game to check progress or if a new player
    {
        if(File.Exists(path))
        {
            Debug.Log("Loaded prexisting save.");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            //Writing all the save data pulled in over to the local static data
            StaticData.firstLoad = data.firstLoad;
            StaticData.shavoUnlock = data.shavoUnlock;
            StaticData.daronUnlock = data.daronUnlock;
            StaticData.serjUnlock = data.serjUnlock;
            StaticData.controlScheme = data.controlScheme;
            StaticData.firstPlay = data.firstPlay;

            return data;
            
        }
        else
        {
            Debug.Log("Created new save where there wasn't.");
            //SaveData data = SaveGame();
            return null;
        }
    }
}
