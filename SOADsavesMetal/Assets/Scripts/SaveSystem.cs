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

        Debug.Log("Control Scheme: " + data.controlScheme + " " + data.firstLoad + " " + data.firstPlay + " " + data.shavoUnlock + " " + data.daronUnlock + " " + data.serjUnlock);


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
            Debug.Log("Save Data FL: " + data.firstLoad);
            Debug.Log("Save Data SHU: " + data.shavoUnlock);
            Debug.Log("Static Data FL before: " + StaticData.firstLoad);
            Debug.Log("Static Data SHU before: " + StaticData.shavoUnlock);
            StaticData.firstLoad = data.firstLoad;
            StaticData.shavoUnlock = data.shavoUnlock;
            StaticData.daronUnlock = data.daronUnlock;
            StaticData.serjUnlock = data.serjUnlock;
            StaticData.controlScheme = data.controlScheme;
            StaticData.firstPlay = data.firstPlay;
            Debug.Log("Static Data FL after: " + StaticData.firstLoad);
            Debug.Log("Static Data SHU after: " + StaticData.shavoUnlock);
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
