using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CameraController : MonoBehaviour
{
    [Header("| Camera")][SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrenght;
    [SerializeField] private float shakeRandomness;
    [SerializeField] private int shakeVibrato;

    [SerializeField] Ease ease;
    [SerializeField] Vector3 changePos;
    [SerializeField] Vector3 changePosFinish;

    [SerializeField] float openTime;

    private EventData eventData;

    [Button("ShakeCam")]
    void ShakeCam()
    {
        Camera.main.transform.DOShakeRotation(shakeDuration, shakeStrenght, shakeVibrato, shakeRandomness);
    }

    private void Awake()
    {
        eventData = Resources.Load("EventData") as EventData;
    }
    public void ChangeCamStage(CameraMoveType camStage)
    {
        switch (camStage)
        {
            case CameraMoveType.Down:
                transform.DOLocalMove(transform.localPosition - changePos, openTime).SetEase(ease);
                break;
            case CameraMoveType.Up:
                transform.DOLocalMove(transform.localPosition + changePos, openTime).SetEase(ease);
                break;
            case CameraMoveType.FinishPosition:
                transform.DOLocalMove(transform.localPosition + changePosFinish, openTime * 2).SetEase(ease);
                transform.DORotate(Vector3.right * 5, openTime * 2);
                break;
            default:
                break;
        }
    }
    public void CameraShake()
    {
        Camera.main.transform.DOShakeRotation(shakeDuration, shakeStrenght, shakeVibrato, shakeRandomness);
    }
}
