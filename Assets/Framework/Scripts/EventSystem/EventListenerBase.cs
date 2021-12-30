using UnityEngine;
using UnityEngine.Events;

namespace Framework.EventSystem
{
    public abstract class EventListenerBase<T, E, UE> : MonoBehaviour, IEventListener<T> where E : GameEventBase<T> where UE : UnityEvent<T>
    {
        [SerializeField] private E eventRaised;
        public UE response;
        
        public void OnEventRaised(T data)
        {
            response?.Invoke(data);
        }
        
        private void OnEnable()
        {
            if (eventRaised == null) return;
            eventRaised.RegisterListener(this);
        }
        
        private void OnDisable()
        {
            if (eventRaised == null) return;
            eventRaised.UnregisterListener(this);
        }
    }
}