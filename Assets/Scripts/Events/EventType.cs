

//Defines the different event types to be used in event data in enumeration form
namespace Events
{
    public enum EventType 
    {
        SendBlobScore,
        SetScoreStats,
        UpdateCurrentScore,

        PlayerAttemptLevelCompletion,
        DidPlayerCompleteLevel,
        PlayerDied,
    }
}
