namespace Framework.EventSystem
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IEventListener<T>
    {
        void OnEventRaised(T data);
    }
}