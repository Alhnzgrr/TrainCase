using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    [SerializeField] DynamicJoystick dynamicJoystick;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float boundary;

    Rigidbody _rigidbody;

    float horizontal;
    float fuel = 100f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(fuel > 100) fuel = 100;

        horizontal = dynamicJoystick.Horizontal;
        fuel -= Time.deltaTime * 2f;
    }
    private void FixedUpdate()
    {

        if (transform.position.x < -boundary && horizontal < 0
            ||
            transform.position.x > boundary && horizontal > 0)
        {
            _rigidbody.velocity = Vector3.forward * verticalSpeed * Time.deltaTime;
        }
        else
        {
            TrainMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Debug.Log("Calisti");
            other.transform.DOMoveY(20, 0.25f).SetEase(Ease.InFlash);
            
            fuel += 10f;
        }
    }

    private void TrainMove()
    {
        _rigidbody.velocity = new Vector3(horizontal * horizontalSpeed, 0, verticalSpeed) * Time.deltaTime;
    }
}
