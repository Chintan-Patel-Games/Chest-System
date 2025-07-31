using System;

namespace ChestSystem.Events
{
    public class EventController<T>
    {
        public event Action<T> baseEvent;
        public void InvokeEvent(T arg) => baseEvent?.Invoke(arg);
        public void AddListener(Action<T> listener) => baseEvent += listener;
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }
}