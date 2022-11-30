//Event that informs subscribers of a debug log

namespace Events
{
    public class SendBlobScore : EventData
    {
        public float BlobScore;

        public SendBlobScore(float blobScore) : base(EventType.SendBlobScore)
        {
            BlobScore = blobScore;
        }
    }
    
    public class SetScoreStats : EventData
    {
        public float TotalScore;
        public float StartingScore;

        public SetScoreStats(float totalScore,float startingScore) : base(EventType.SendBlobScore)
        {
            StartingScore = startingScore;
            TotalScore = totalScore;
        }
    }
    
    public class UpdateCurrentScore : EventData
    {
        public float CurrentScore;

        public UpdateCurrentScore(float currentScore) : base(EventType.SendBlobScore)
        {
            CurrentScore = currentScore;
        }
    }
}