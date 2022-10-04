using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoSingleton<UIController>
{
    private EventData _eventData;

    [Header("Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject gamePanel;
    
    [Header("Buttons")]
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button playButton;
    
    [Header("Input")] 
    [SerializeField] private DynamicJoystick dynamicJoystick;

    [Header("Image")] 
    [SerializeField] private Image fuelBar;
    [SerializeField] private Image fuelImage;
    [SerializeField] private Image victoryImage;
    [SerializeField] private Image gameOverImage;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI victoryScore;

    private bool canFuelImageAnimation;

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
        gamePanel.SetActive(false);
        StartCoroutine(VictoryPanelOpen());
    }

    private void Over()
    {
        gamePanel.gameObject.SetActive(false);
        StartCoroutine(LosePanelOpen());
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
    public void ScoreTextUpdate(int scoreValue)
    {
        scoreText.text = $"Score {scoreValue}";
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
    public void PlayFuelAnimation()
    {
        if (canFuelImageAnimation) return;

        canFuelImageAnimation = true;
        fuelImage.transform.DOScale(Vector3.one * 0.7f, 0.2f).SetEase(Ease.Flash).OnComplete(() => fuelImage.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutElastic).OnComplete(()=>canFuelImageAnimation = false));

    }
    IEnumerator VictoryPanelOpen()
    {
        victoryImage.transform.localScale = Vector3.zero;
        nextLevelButton.transform.localScale = Vector3.zero;
        victoryPanel.gameObject.SetActive(true);
        victoryImage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= (GameManager.Instance.Score * 10) ; i++)
        {
            victoryScore.text = $"Score {i}";

            yield return new WaitForSecondsRealtime(0.001f);
        }
        victoryScore.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.OutElastic).OnComplete(() => victoryScore.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic));
        Transform[] stars = victoryImage.GetComponentsInChildren<Transform>();
        foreach (Transform star in stars)
        {
            star.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
            yield return new WaitForSeconds(0.4f);
        }

        _eventData.OnExplodeFireworks?.Invoke();
        
        yield return new WaitForSeconds(0.5f);

        nextLevelButton.transform.DOScale(Vector3.one , 1f).SetEase(Ease.OutBounce);
    }
    IEnumerator LosePanelOpen()
    {
        gameOverImage.transform.localScale = Vector3.zero;
        tryAgainButton.transform.localScale = Vector3.zero;
        losePanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        gameOverImage.transform.DOScale(Vector3.one * 1.2f, 1f).SetEase(Ease.OutElastic).OnComplete(() => gameOverImage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic));

        yield return new WaitForSeconds(1.5f);

        tryAgainButton.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
    }
}
