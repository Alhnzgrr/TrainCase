using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    private EventData _eventData;
    
    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;
    [SerializeField] int goldCoefficient;

    private int score;
    public int Score => score;

    GameState _gameState = GameState.Idle;

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

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;

        Singleton(true);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(Level);
        }
    }

    private void OnEnable()
    {
        _eventData.OnFinish += Finish;
        _eventData.OnOver += Over;
        _eventData.OnPlay += StartLevel;
    }
    private void OnDisable()
    {
        _eventData.OnFinish -= Finish;
        _eventData.OnOver -= Over;
        _eventData.OnPlay -= StartLevel;
    }

    private void Finish()
    {
        _gameState = GameState.Finish;
    }

    private void Over()
    {
        _gameState = GameState.Over;
    }

    private void StartLevel()
    {
        _gameState = GameState.Play;
    }
    
    public void NextLevel()
    {
        _gameState = GameState.Idle;
        score = 0;
        Level++;
        SceneManager.LoadScene(Level);
    }
    
    public void RestartLevel()
    {
        _gameState = GameState.Idle;
        score = 0;
        SceneManager.LoadScene(Level);
    }
    public void ScoreUpdate(int getScore)
    {
        score++;
        UIController.Instance.ScoreTextUpdate(score);
    }
}
