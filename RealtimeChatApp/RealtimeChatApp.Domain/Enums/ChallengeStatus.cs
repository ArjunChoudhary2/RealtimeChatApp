namespace RealtimeChatApp.RealtimeChatApp.Domain.Enums
{
    public enum ChallengeStatus
    {
        Pending,   // The challenge has been issued but not yet responded to.
        Completed, // The challenge was fulfilled.
        Failed     // The challenge was not fulfilled within the time limit.
    }
}
