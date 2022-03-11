using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    [SerializeField] private GameEvent gameEvent = default;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent OnEventRaised;

    private void OnEnable() =>  gameEvent.OnEventRaised += Respond;

    private void OnDisable() => gameEvent.OnEventRaised -= Respond;


    private void Respond()
    {
        OnEventRaised?.Invoke();
    }
}