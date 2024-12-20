using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.Dtos;

public class UserDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GroupStatus GroupStatus { get; set; }
}
