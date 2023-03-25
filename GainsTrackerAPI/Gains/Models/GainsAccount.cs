using GainsTrackerAPI.Security.Models;

namespace GainsTrackerAPI.Gains.Models;

public class GainsAccount
{
    public string UserName { get; set; } = "";
    public List<Workout> Workouts { get; set; }

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public User User { get; set; }

    #endregion
}
