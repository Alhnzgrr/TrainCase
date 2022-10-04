using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Collactable : MonoBehaviour
{
    [SerializeField] private CollactableType collectaleType;
    bool onPlayer = false;
    public bool OnPlayer => onPlayer;
    public CollactableType CollectaleType => collectaleType;
    public void DestroyEffect()
    {
        transform.DOScale(Vector3.zero, 0.5f);
    }
    public void ThrowObject()
    {
        transform.DOScale(Vector3.zero, 1f);
        transform.DOJump(transform.position + Vector3.up * 100, 1f , 1 , 2f);
    }
    public void OnPlayerTrue()
    {
        onPlayer = true;
    }
}