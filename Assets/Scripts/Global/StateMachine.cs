using System;
using System.Collections.Generic;

public class StateMachine<T> where T : Enum
{
    private readonly Dictionary<T, (Action, Action, Action)> _dictionary;
    private T _currentState;

    public StateMachine(T initialState)
    {
        Locked = false;
        _dictionary = new Dictionary<T, (Action, Action, Action)>();

        foreach (T state in Enum.GetValues(typeof(T))) _dictionary.Add(state, (null, null, null));

        CurrentState = initialState;
    }

    public T CurrentState
    {
        get => _currentState;
        set
        {
            if (Locked) return;
            _dictionary[_currentState].Item3?.Invoke();
            _currentState = value;
            _dictionary[_currentState].Item1?.Invoke();
        }
    }

    public bool Locked { get; set; }

    public void OnEnterState(T state, Action action)
    {
        var stateActions = _dictionary[state];
        stateActions.Item1 = action;
        _dictionary[state] = stateActions;
    }

    public void OnStayState(T state, Action action)
    {
        var stateActions = _dictionary[state];
        stateActions.Item2 = action;
        _dictionary[state] = stateActions;
    }

    public void OnExitState(T state, Action action)
    {
        var stateActions = _dictionary[state];
        stateActions.Item3 = action;
        _dictionary[state] = stateActions;
    }

    public void Update()
    {
        _dictionary[_currentState].Item2?.Invoke();
    }
}