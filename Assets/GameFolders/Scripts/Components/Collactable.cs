using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collactable : MonoBehaviour
{
    [SerializeField] private CollactableType collectaleType;

    public CollactableType CollectaleType => collectaleType;

}