using System.ComponentModel.DataAnnotations.Schema;

namespace GainsTracker.Data.Friends.Entities;

[Table("friend")]
public class FriendEntity
{
    public FriendEntity() { }
    
    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }
    public DateTime FriendsSince { get; set; }
    public required string FriendName { get; set; }
    public required string FriendHandle { get; set; }
}