using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private CollactableType leftDoorType;
    [SerializeField] private CollactableType rightDoorType;
    [SerializeField] private GameObject human;
    [SerializeField] private GameObject coal;
    
    public CollactableType GetCollectableType(Transform trainTransform)
    {
        if (trainTransform.localPosition.x < 0)
        {
            return leftDoorType;
        }
        else
        {
            return rightDoorType;
        }
    }

    private void OnValidate()
    {
        if (human)
        {
            if (leftDoorType == CollactableType.Human)
            {
                human.transform.localPosition = Vector3.right * -8f;
            }
            else
            {
                human.transform.localPosition = Vector3.right * 8f;
            }
        }

        if (coal)
        {
            if (leftDoorType == CollactableType.Coal)
            {
                coal.transform.localPosition = Vector3.right * -8f;
            }
            else
            {
                coal.transform.localPosition = Vector3.right * 8f;
            }
        }

        if (leftDoorType == CollactableType.Human)
        {
            rightDoorType = CollactableType.Coal;
        }
        else
        {
            rightDoorType = CollactableType.Human;
        }
        
    }
}
