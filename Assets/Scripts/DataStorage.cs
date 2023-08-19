using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    //public string playerName;
    //public int levelReached;
    //public int score;
    public float sfxVolume;
    public float musicVolume;
}

public class DataStorage : MonoBehaviour
{
    private string dataPath;

    private void Start()
    {

        dataPath = Path.Combine(Application.persistentDataPath, "questmanData.json");
        Debug.Log(dataPath);
    }

    public void SaveGameData(float sfxVolume, float musicVolume) //string playerName, int levelReached, int score, 
    {
        GameData playerData = new GameData
        {
            //playerName = playerName,
            //levelReached = levelReached,
            //score = score,
            sfxVolume = sfxVolume,
            musicVolume = musicVolume
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(dataPath, json);
    }

    public GameData LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<GameData>(json);
        }

        return null;
    }
}
