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
        public readonly float RequiredScore;
        public readonly float StartingScore;
        public readonly float TimeLimit;

        public SetScoreStats(float requiredScore, float startingScore, float timeLimit) : base(EventType.SetScoreStats)
        {
            StartingScore = startingScore;
            RequiredScore = requiredScore;
            TimeLimit = timeLimit;
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

    public class PlayerAttemptLevelCompletion : EventData
    {
        public PlayerAttemptLevelCompletion() : base(EventType.PlayerAttemptLevelCompletion)
        {
        }
    }

    public class DidPlayerCompleteLevel : EventData
    {
        public readonly bool DidComplete;
        public readonly float FinalScore;

        public DidPlayerCompleteLevel(bool didComplete, float finalScore) : base(EventType.DidPlayerCompleteLevel)
        {
            DidComplete = didComplete;
            FinalScore = finalScore;
        }
    }

    public class PlayerDied : EventData
    {
        public PlayerDied() : base(EventType.PlayerDied)
        {
        }
    }
}