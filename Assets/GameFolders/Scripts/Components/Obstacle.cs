using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    [SerializeField] LoopType loopType;
    [SerializeField] int loop;
    [SerializeField] float moveX;
    [SerializeField] float moveTime;
    [SerializeField] float rotateMoveSpeed;

    bool crashTrain = false;

    public bool CrashTrain => crashTrain;
    private void Start()
    {
        transform.DOMoveX(moveX, moveTime).SetLoops(loop, loopType);
    }
    private void Update()
    {
        transform.Rotate(0, 0, rotateMoveSpeed * Time.deltaTime);
    }

    public void DestroyEffect()
    {
        crashTrain = true;
        transform.DOScale(Vector3.zero, 0.25f);
        Destroy(gameObject, 0.25f);
    }

}
