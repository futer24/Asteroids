using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EventInt : UnityEvent<int>
{

}
public class GameEventIntListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    [SerializeField] private GameEventInt gameEventInt = default;

    [Tooltip("Response to invoke when Event is raised.")]
    public EventInt OnEventRaised;

    private void OnEnable() => gameEventInt.OnEventRaised += Respond;

    private void OnDisable() => gameEventInt.OnEventRaised -= Respond;


    private void Respond(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
