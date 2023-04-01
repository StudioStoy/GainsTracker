namespace GainsTrackerAPI.Gains.Models;

public class GainsAccount
{
    public string Username { get; set; } = "";
    public List<Workout> Workouts { get; set; }

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }

    #endregion
}
