namespace GainsTrackerAPI.Gains.Models;

public class Workout
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public GainsAccount GainsAccount { get; set; }

    public string Type { get; set; }
    public int PersonalBest { get; set; }
}
