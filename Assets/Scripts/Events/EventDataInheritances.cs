//Event that informs subscribers of a debug log

namespace Events
{
    /// <summary>
    /// Updates the displayed combo score ui
    /// </summary>
    public class SendBlobScore : EventData
    {
        public float BlobScore;

        public SendBlobScore(float blobScore) : base(EventType.SendBlobScore)
        {
            BlobScore = blobScore;
        }
    }
}