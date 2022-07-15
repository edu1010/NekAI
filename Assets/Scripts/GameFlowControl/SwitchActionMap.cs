using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchActionMap : MonoBehaviour
{
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnEnable()
    {
        GameFlowController.Instance.OnSwitch += switchMap;
    }
    private void OnDisable()
    {
        GameFlowController.Instance.OnSwitch -= switchMap;
    }
    public void switchMap(string map)
    {
        _playerInput.SwitchCurrentActionMap(map);
        Debug.Log("cambio");
    }
}
