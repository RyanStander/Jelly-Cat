//Event that informs subscribers of a debug log

namespace Events
{
    public class SendBlobScore : EventData
    {
        public readonly float BlobScore;

        public SendBlobScore(float blobScore) : base(EventType.SendBlobScore)
        {
            BlobScore = blobScore;
        }
    }
    
    public class SetScoreStats : EventData
    {
        public readonly float TotalScore;
        public readonly float StartingScore;

        public SetScoreStats(float totalScore,float startingScore) : base(EventType.SetScoreStats)
        {
            StartingScore = startingScore;
            TotalScore = totalScore;
        }
    }
    
    public class UpdateCurrentScore : EventData
    {
        public readonly float CurrentScore;

        public UpdateCurrentScore(float currentScore) : base(EventType.UpdateCurrentScore)
        {
            CurrentScore = currentScore;
        }
    }
    
    public class SetTime : EventData
    {
        public readonly float Time;

        public SetTime(float time) : base(EventType.SetTime)
        {
            Time = time;
        }
    }
    
    public class TimerExpired : EventData
    {
        public TimerExpired() : base(EventType.TimerExpired)
        {
        }
    }
}