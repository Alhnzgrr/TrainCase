using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/Movement Data")]
public class MovementData : ScriptableObject
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalFollowSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float verticalAcceleration;
    [SerializeField] private Vector2 horizontalBounds;
    [SerializeField] private float startFuelAmount;
    [SerializeField] private float maxFuelAmount;
    [SerializeField] private float startTrainWeight;
    [SerializeField] private float eachCarriageWaieght;
    [SerializeField] private int scoreUpdateValue;


    public float HorizontalSpeed => horizontalSpeed;
    public float HorizontalFollowSpeed => horizontalFollowSpeed;
    public float VerticalSpeed => verticalSpeed;
    public float VerticalAcceleration => verticalAcceleration;
    public float StartFuelAmount => startFuelAmount;
    public float MaxFuelAmount => maxFuelAmount;
    public Vector2 HorizontalBounds => horizontalBounds;
    public float StartTrainWeight => startTrainWeight;
    public float EachCarriageWaieght => eachCarriageWaieght;
    public int ScoreUpdateValue => scoreUpdateValue;

}
