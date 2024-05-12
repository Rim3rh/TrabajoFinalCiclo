using UnityEngine.Events;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    //create the reference to the event you wil be subcribing to
    public GameEvent Event;
    //create reference to acction that will happen on event raised
    public UnityEvent Response;

    //on enable add this obj as listener of the event
    private void OnEnable()
    { Event.RegisterListener(this); }
    //on disable remove this obj as listener of the event
    private void OnDisable()
    { Event.UnregisterListener(this); }
    //called from event"" will reaise the response events added from inspector
    public void OnEventRaised()
    { Response.Invoke(); }
}