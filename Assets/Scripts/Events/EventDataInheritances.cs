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
}