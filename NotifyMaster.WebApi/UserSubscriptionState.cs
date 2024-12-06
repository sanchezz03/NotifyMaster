namespace NotifyMaster.WebApi;

public class UserSubscriptionState
{
    public long ChatId { get; set; }
    public long UserId { get; set; }
    public int Attempt { get; set; } = 0;
    public DateTime NextCheckTime { get; set; } 
}
