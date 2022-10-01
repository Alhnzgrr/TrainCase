using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using GameAnalyticsSDK;

public class GameManager : MonoSingleton<GameManager>
{
    private EventData eventData;
    
    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;
    [SerializeField] int goldCoefficient;
    
    GameState _gameState = GameState.Play;

    public GameState GameState
    {
        get => _gameState;
        set =>  _gameState = value;
    }

    public bool Playability()
    {
        return _gameState == GameState.Play;
    }

    public int Level
    {
        get
        {
            if (PlayerPrefs.GetInt("Level") == 0)
            {
                PlayerPrefs.SetInt("Level", 1);
                PlayerPrefs.SetInt("EndlessLevel", 1);
                return PlayerPrefs.GetInt("Level");
            }
            else if (PlayerPrefs.GetInt("Level") > levelCount)
            {
                return Random.Range(randomLevelLowerLimit, levelCount);
            }
            else
            {
                return PlayerPrefs.GetInt("Level");
            }
        }
        set => PlayerPrefs.SetInt("Level", value);
    }

    public int Gold
    {
        get => PlayerPrefs.GetInt("Gold");
        set => PlayerPrefs.SetInt("Gold", value);
    }

    public int OstensibleLevel
    {
        get => PlayerPrefs.GetInt("OstensibleLevel");
        set => PlayerPrefs.SetInt("OstensibleLevel", value);
    }

    public int Score
    {
        get => PlayerPrefs.GetInt("Score");
        set => PlayerPrefs.SetInt("Score", value);
    }

    private void Awake()
    {
        Singleton(true);
        GameAnalytics.Initialize();
        GameAnalytics.NewDesignEvent("Game Start");
    }

    private void Start()
    {
        eventData = Resources.Load("EventData") as EventData;
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(Level);
        }
    }
    
    public void NextLevel()
    {
        _gameState = GameState.Play;
        SceneManager.LoadScene(Level);
    }
}
