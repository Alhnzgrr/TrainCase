using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    private EventData _eventData;
    private Carriage[] _carriages;
    private MovementData _movementData;
    private Rigidbody _rigidbody;
    private Transform locomotive;

    private CollactableType _collactableType;

    private float speed;
    private float _fuel;
    private float _trainWeight;
    private int carriageIndex = 0;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
        _movementData = Resources.Load("MovementData") as MovementData;
        
        _rigidbody = GetComponent<Rigidbody>();
        _carriages = GetComponentsInChildren<Carriage>();
    }

    private void OnEnable()
    {
        _eventData.OnPlay += StartGame;
    }

    private void OnDisable()
    {
        _eventData.OnPlay -= StartGame;
    }

    private void Start()
    {
        SetFollowersTargets();
        
        _fuel = _movementData.StartFuelAmount;
        _trainWeight = _movementData.StartTrainWeight;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.FUEL))
        {
            _fuel += other.GetComponent<FuelBarrel>().GetFuelAmount();
            Destroy(other.gameObject);
        }

        if (other.CompareTag(Constants.Tags.OBSTACLE))
        {
            // Obstacle Process
        }

        if (other.CompareTag(Constants.Tags.COLLECTABLE))
        {
            if (other.GetComponent<Collactable>().CollectaleType == _collactableType)
            {
                // Doğru seçim
                OpenCarraigeItem();
            }
            else
            {
                // yanlış seçim
                // Give feedback
            }
            
            Destroy(other.gameObject);
        }

        if (other.CompareTag(Constants.Tags.DOOR))
        {
            _collactableType = other.GetComponent<Door>().GetCollectableType(locomotive);

            foreach (Carriage carriage in _carriages)
            {
                carriage.SetItemType(_collactableType);
            }
        }

        if (other.CompareTag(Constants.Tags.CARRIGE))
        {
            if (carriageIndex < _carriages.Length)
            {
                _carriages[carriageIndex].OpenCarriage();
                carriageIndex++;
                _trainWeight += _movementData.EachCarriageWaieght;
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag(Constants.Tags.FINISH))
        {
            _eventData.OnFinish?.Invoke();
        }
    }

    private void Update()
    {
        if (!CanMove()) return;

        _fuel -= _trainWeight * Time.deltaTime;
        UIController.Instance.SetFuelBarValue(_fuel / _movementData.MaxFuelAmount);
    }

    private void FixedUpdate()
    {
        if (!CanMove()) return;

        Move();
    }

    private void StartGame()
    {
        StartCoroutine(SpeedUp());
    }
    
    private void SetFollowersTargets()
    {
        locomotive = GetComponentInChildren<HorizontalLocalMove>().transform;
        _carriages[0].Target = locomotive;
        
        for (int i = 1; i < _carriages.Length; i++)
        {
            _carriages[i].Target = _carriages[i - 1].transform;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.forward * speed;
    }

    private void OpenCarraigeItem()
    {
        for (int i = 0; i < _carriages.Length; i++)
        {
            if (!_carriages[i].IsCarriageFull()) break;
        }
    }

    private bool CanMove()
    {
        if (GameManager.Instance.Playability() && _fuel <= 0)
        {
            _eventData.OnOver?.Invoke();
            return false;
        }
        else if (GameManager.Instance.Playability() && _fuel > 0)
        {
            return true;
        }
        
        return false;
    }


    IEnumerator SpeedUp()
    {
        while (speed < _movementData.VerticalSpeed)
        {
            speed += Time.deltaTime * _movementData.VerticalAcceleration;

            yield return null;
        }

        speed = _movementData.VerticalSpeed;
    }
}