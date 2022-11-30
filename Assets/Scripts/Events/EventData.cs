

//Base class type that allows for the creation of event datas
namespace Events
{
    public class EventData
    {
        public readonly EventType eventType;

        public EventData(EventType type)
        {
            eventType = type;
        }
    }
}
