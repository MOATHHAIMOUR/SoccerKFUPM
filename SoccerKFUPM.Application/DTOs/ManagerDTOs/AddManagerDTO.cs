using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;

namespace SoccerKFUPM.Application.DTOs.ManagerDTOs;

public class AddManagerDTO
{
    public required string KFUPMId { get; set; }
    public required string FirstName { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string IntialPassword { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string? ThirdName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public int? NationalityId { get; set; }
    public required List<PersonalContactInfoDTO> PersonalContactInfos { get; set; }
}