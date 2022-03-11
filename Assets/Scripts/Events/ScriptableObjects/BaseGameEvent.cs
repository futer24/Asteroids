using UnityEngine.Events;
using UnityEngine;

public abstract class BaseGameEvent : BaseGameEventScriptable
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}

public abstract class BaseGameEvent<T> : BaseGameEventScriptable
{
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T input)
    {
        OnEventRaised?.Invoke(input);
    }
}
