using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlightController2D : MonoBehaviour
{
    [SerializeField] private float flyingSpeed, flyingTime, reloadTime;

    [SerializeField] private CollisionDetector2D groundCheck, wallCheck;

    [SerializeField] [ReadOnly] private bool isFlying;

    [SerializeField] [Range(0, 1)] [ReadOnly]
    private float fuelPercentage;

    [SerializeField] [ReadOnly] private float decrementOverTime, incrementOverTime;

    private Rigidbody2D _rigidbody;

    public float FuelPercentage
    {
        get => fuelPercentage;
        set => fuelPercentage = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        fuelPercentage = 1;
        decrementOverTime = Time.fixedDeltaTime / flyingTime;
        incrementOverTime = Time.fixedDeltaTime / reloadTime;
    }

    private void FixedUpdate()
    {
        if (isFlying)
        {
            if (fuelPercentage <= 0f)
            {
                isFlying = false;
                OnStopFlying?.Invoke();
            }
            else
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, flyingSpeed);
                fuelPercentage -= decrementOverTime;
            }
        }
        else if (fuelPercentage < 1 && (wallCheck.IsColliding || groundCheck.IsColliding))
        {
            fuelPercentage += incrementOverTime;
        }

        OnFuelUpdate?.Invoke(fuelPercentage);
    }

    public event Action OnStartFlying, OnStopFlying;

    public void StartFlying()
    {
        if (fuelPercentage < 0.1f) return;
        isFlying = true;
        OnStartFlying?.Invoke();
    }

    public void StopFlying()
    {
        if (!isFlying) return;
        isFlying = false;
        OnStopFlying?.Invoke();
    }

    public event Action<float> OnFuelUpdate;
}