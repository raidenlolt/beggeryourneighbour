using System.Collections.Generic;
using UnityEngine;

namespace Framework.EventSystem
{
    public abstract class GameEventBase<T> : ScriptableObject
    {
        private readonly List<IEventListener<T>> listeners = new List<IEventListener<T>>();
        
        public virtual void Raise(T data)
        {
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(data);
            }
        }
        
        public void RegisterListener(IEventListener<T> listener)
        {
            if (!listeners.Contains(listener)) listeners.Add(listener);
        }
        
        public void UnregisterListener(IEventListener<T> listener)
        {
            if (listeners.Contains(listener)) listeners.Remove(listener);
        }
    }
}