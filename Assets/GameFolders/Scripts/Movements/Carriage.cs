using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : MonoBehaviour
{
    [SerializeField] private List<Transform> humans;
    [SerializeField] private List<Transform> coals;
    
    private MovementData _movementData;
    private MeshRenderer _meshRenderer;
    private Transform _target;

    private bool isFull;
    private CollactableType itemType;
    private int count;
    private int activeIndex;
    
    public Transform Target
    {
        get => _target;
        set => _target = value;
    }
    
    private void Awake()
    {
        _movementData = Resources.Load("MovementData") as MovementData;

        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.enabled = false;
        count = humans.Count;
    }

    private void Update()
    {
        float localX = Mathf.Lerp(transform.localPosition.x, _target.localPosition.x, _movementData.HorizontalFollowSpeed * Time.deltaTime);
        
        transform.localPosition = new Vector3(localX, transform.localPosition.y, transform.localPosition.z);
    }

    public void OpenCarriage()
    {
        _meshRenderer.enabled = true;
    }

    public void SetItemType(CollactableType type)
    {
        itemType = type;
    }

    public bool IsCarriageFull()
    {
        if (activeIndex >= count)
        {
            return true;
        }
        else
        {
            // Yeni item aç
            if (itemType == CollactableType.Coal)
            {
                coals[activeIndex].gameObject.SetActive(true);
            }
            else
            {
                humans[activeIndex].gameObject.SetActive(true);
            }
            
            activeIndex++;
            return false;
        }
    }
}
