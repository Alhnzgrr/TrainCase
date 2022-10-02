using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    private EventData _eventData;

    [Header("Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;
    
    [Header("Buttons")]
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button playButton;
    
    [Header("Input")] 
    [SerializeField] private DynamicJoystick dynamicJoystick;

    [Header("Fuel")] 
    [SerializeField] private Image fuelBar;
    
    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;

        Singleton();
        
        nextLevelButton.onClick.AddListener(NextLevel);
        tryAgainButton.onClick.AddListener(TryAgain);
        playButton.onClick.AddListener(StartGame);
    }

    private void OnEnable()
    {
        _eventData.OnFinish += Finish;
        _eventData.OnOver += Over;
    }

    private void OnDisable()
    {
        _eventData.OnFinish -= Finish;
        _eventData.OnOver -= Over;
    }

    private void Finish()
    {
        victoryPanel.SetActive(true);
    }

    private void Over()
    {
        losePanel.SetActive(true);
    }

    private void StartGame()
    {
        _eventData.OnPlay?.Invoke();
        playButton.gameObject.SetActive(false);
    }

    private void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }

    private void TryAgain()
    {
        GameManager.Instance.RestartLevel();
    }
    
    public Vector2 GetJoystickDirection()
    {
        return dynamicJoystick.Direction;
    }

    public float GetJoystickHorizontal()
    {
        return dynamicJoystick.Horizontal;
    }

    public float GetJoystickVertical()
    {
        return dynamicJoystick.Vertical;
    }

    public void SetFuelBarValue(float value)
    {
        fuelBar.fillAmount = value;
    }
}
