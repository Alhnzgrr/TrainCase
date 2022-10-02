using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLocalMove : MonoBehaviour
{
    private MovementData _movementData;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _movementData = Resources.Load("MovementData") as MovementData;

        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!CanMove()) return;

        transform.localPosition += Vector3.right * (_movementData.HorizontalSpeed * UIController.Instance.GetJoystickHorizontal() * Time.deltaTime);
    }

    private bool CanMove()
    {
        if (!GameManager.Instance.Playability()) return false;
        
            if ((transform.localPosition.x < _movementData.HorizontalBounds.x && UIController.Instance.GetJoystickHorizontal() < 0 || transform.localPosition.x > _movementData.HorizontalBounds.y && UIController.Instance.GetJoystickHorizontal() > 0))
        {
            return false;
        }
        else
        {
            return true;
        }

        
    }
}