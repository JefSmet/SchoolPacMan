using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public float sfxVolume;
    public float musicVolume;
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;

    public PlayerScore(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }

}
[System.Serializable]
public class PlayerScoreList
{
    public List<PlayerScore> scores;

    public PlayerScoreList(List<PlayerScore> scores)
    {
        this.scores = scores;
    }
}


public class DataStorage : MonoBehaviour
{
    private string settingsPath;
    private string scorePath;

    private void Start()
    {

        settingsPath = Path.Combine(Application.persistentDataPath, "questmanSettingsData.json");
        scorePath = Path.Combine(Application.persistentDataPath, "questmanScoreData.json");
        Debug.Log(settingsPath);
    }

    public void SaveGameData(float sfxVolume, float musicVolume)
    {
        GameData playerData = new GameData
        {

            sfxVolume = sfxVolume,
            musicVolume = musicVolume
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(settingsPath, json);
    }
    public void SaveScores(PlayerScoreList playerScores)
    {
        string json = JsonUtility.ToJson(playerScores);
        System.IO.File.WriteAllText(scorePath, json);
    }

    public PlayerScoreList LoadScores()
    {
        if (System.IO.File.Exists(scorePath))
        {
            string json = System.IO.File.ReadAllText(scorePath);
            PlayerScoreList loadedScores = JsonUtility.FromJson<PlayerScoreList>(json);
            return loadedScores;
        }
        return null;

    }


    public GameData LoadGameData()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            return JsonUtility.FromJson<GameData>(json);
        }

        return null;
    }
}
