using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : SingletonPersistent<GameManager>
{
    public event Action<int> onLivesChanged;
    [SerializeField] private GameObject _arduinoControllerPrefab;
    [SerializeField] private DataStorage _dataStorage;
    [SerializeField] private List<String> levels = new List<String>();
    private int currentLevelIndex=0;

    private SerialCommThreaded _arduinoController;

    private int lives;

    public PlayerScoreList playerScores = new PlayerScoreList(new List<PlayerScore>());

    public int GameScore { get; set; }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            onLivesChanged?.Invoke(lives);
        }
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }


    public SerialCommThreaded ArduinoController
    {
        get
        {
            if (_arduinoController == null)
            {
                if (_arduinoControllerPrefab == null)
                {
                    _arduinoController = gameObject.AddComponent<SerialCommThreaded>();
                }
                else
                {
                    _arduinoController = _arduinoControllerPrefab.GetComponent<SerialCommThreaded>();
                }
            }
            return _arduinoController;
        }
    }



    private void Start()
    {
        GameData gameData = _dataStorage.LoadGameData();

        playerScores = _dataStorage.LoadScores();
        if (gameData != null)
        {
            AudioManager.Instance.SetVolumeSFX(gameData.sfxVolume);
            AudioManager.Instance.SetVolumeMusic(gameData.musicVolume);
        }
        if (SceneManager.GetActiveScene().name.Equals("BootScene"))
        {
            LoadSceneAfterDelay("MainMenu");
        }

        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        GameScore = 0;
        Lives = 3;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitGame()
    {
        _dataStorage.SaveGameData(AudioManager.Instance.sfx.volume, AudioManager.Instance.ambientMusic.volume);
        Application.Quit();
    }

    public void LoadSceneAfterDelay(string sceneName)
    {
        StartCoroutine(DelayedSceneLoad(sceneName));
    }

    IEnumerator DelayedSceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }


    public string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void AddAndSort(PlayerScore newScore)
    {
        int index = playerScores.scores.FindIndex(score => score.score < newScore.score);

        if (index != -1) // Als er een score gevonden is die lager is dan de nieuwe score
        {
            playerScores.scores.Insert(index, newScore);
        }
        else // Anders voeg je de nieuwe score aan het einde van de lijst toe
        {
            playerScores.scores.Add(newScore);
        }
    }






    public bool IsScoreInTop5(int checkScore)
    {
        
        if (playerScores.scores.Count < 5)
        {
            return true;
        }

        // Anders, controleer of checkScore hoger is dan de 5e score in de lijst.
        return checkScore > playerScores.scores[4].score;
    }

    public void SaveScores()
    {
        _dataStorage.SaveScores(playerScores);
    }


    public void LoadNextLevel()
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
            SceneManager.LoadScene(levels[currentLevelIndex]);
        }
        else
        {
            GameOver();
        }
    }
}
