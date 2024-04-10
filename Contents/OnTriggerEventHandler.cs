using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEventHandler : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider> mOnTriggerEnterEvent;
    [SerializeField] UnityEvent<Collider> mOnTriggerStayEvent;
    [SerializeField] UnityEvent<Collider> mOnTriggerExitEvent;

    // Enter
    public void AddListenerEnterEvent(UnityAction<Collider> action)
    {
        mOnTriggerEnterEvent.AddListener(action);
    }
    public void RemoveListenerEnterEvent(UnityAction<Collider> action)
    {
        mOnTriggerEnterEvent.RemoveListener(action);
    }
    //

    // Stay
    public void AddListenerStayEvent(UnityAction<Collider> action)
    {
        mOnTriggerStayEvent.AddListener(action);
    }
    public void RemoveListenerStayEvent(UnityAction<Collider> action)
    {
        mOnTriggerStayEvent.RemoveListener(action);
    }
    //

    // Exit
    public void AddListenerExitEvent(UnityAction<Collider> action)
    {
        mOnTriggerExitEvent.AddListener(action);
    }
    public void RemoveListenerExitEvent(UnityAction<Collider> action)
    {
        mOnTriggerExitEvent.RemoveListener(action);
    }
    //

    private void OnTriggerEnter(Collider other)
    {
        mOnTriggerEnterEvent.Invoke(other);
    }
    void OnTriggerStay(Collider other)
    {
        mOnTriggerStayEvent.Invoke(other);
    }
    void OnTriggerExit(Collider other)
    {
        mOnTriggerExitEvent.Invoke(other);
    }
}
