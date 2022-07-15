using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour, InputActions.IInGameActionsActions
{
    public delegate void OnNextTextDelegate();
    public static OnNextTextDelegate NextText;
    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.ReadValue<Vector2>().x)
        {
            case var value when value > 0:
                OnMoveRight?.Invoke();
                break;
            case var value when value < 0:
                OnMoveLeft?.Invoke();
                break;
            case 0:
                OnStopMoving?.Invoke();
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                OnStartJumping?.Invoke();
                break;
            case var value when context.canceled:
                OnStopJumping?.Invoke();
                break;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        OnStartDash?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        OnStartAttack?.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                OnStartShooting?.Invoke();
                break;
            case var value when context.canceled:
                OnStopShooting?.Invoke();
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        GameFlowController.Instance.PauseGame();
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.performed && context.interaction is HoldInteraction:
                Debug.Log("Healing!");
                OnStartHealing?.Invoke();
                break;
            case var value when context.canceled:
                OnStopHealing?.Invoke();
                break;
        }
    }

    public event Action OnMoveLeft,
        OnMoveRight,
        OnStopMoving,
        OnStartJumping,
        OnStopJumping,
        OnStartDash,
        OnStartAttack,
        OnStartShooting,
        OnStopShooting,
        OnStartHealing,
        OnStopHealing;

    public void OnDie(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Me muero");
            GameObject.FindGameObjectWithTag("Player").GetComponent<BasicDamageTaker>().Die();
        }
    }

    public void OnNextText(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            NextText?.Invoke();
        }
    }
}