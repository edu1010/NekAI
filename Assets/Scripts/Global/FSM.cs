using System;
using System.Collections.Generic;

public class FSM<State> where State : Enum
{
    private State current;
    private bool locked;
    private readonly Dictionary<State, StateActions> states;

    public FSM(State initial)
    {
        states = new Dictionary<State, StateActions>();
        foreach (State state in Enum.GetValues(typeof(State))) states.Add(state, new StateActions());
        current = initial;
        locked = false;
    }

    public void Update()
    {
        var action = states[current].OnStay;
        action?.Invoke();
        //states[current].OnStay?.Invoke();
    }

    public void SetState(State t)
    {
        if (locked) return;
        states[current].OnExit?.Invoke();
        states[current = t].OnEnter?.Invoke();
    }

    public State GetState()
    {
        return current;
    }

    public void OnEnterState(State state, Action action)
    {
        states[state].OnEnter = action;
    }

    public void OnStayState(State state, Action action)
    {
        states[state].OnStay = action;
    }

    public void OnExitState(State state, Action action)
    {
        states[state].OnExit = action;
    }

    public void LockState()
    {
        locked = true;
    }

    public void UnlockState()
    {
        locked = false;
    }

    private class StateActions
    {
        public Action OnEnter, OnStay, OnExit;
    }
}