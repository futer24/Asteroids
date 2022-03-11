using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EventPosition : UnityEvent<Vector3>
{

}

public class GameEventPositionListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    [SerializeField] private GameEventVector3 gameEventVector3 = default;

    [Tooltip("Response to invoke when Event is raised.")]
    public EventPosition OnEventRaised;

    private void OnEnable() => gameEventVector3.OnEventRaised += Respond;

    private void OnDisable() => gameEventVector3.OnEventRaised -= Respond;


    private void Respond(Vector3 position)
    {
        OnEventRaised?.Invoke(position);
    }
}
