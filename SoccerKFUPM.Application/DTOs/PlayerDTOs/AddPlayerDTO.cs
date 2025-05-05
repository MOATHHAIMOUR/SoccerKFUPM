using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.PlayerDTOs;
public class AddPlayerDTO
{
    public required string KFUPMId { get; set; }
    public required string FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? ThirdName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public int? NationalityId { get; set; }
    public required PlayerType PlayerType { get; set; }
    public required int DepartmentId { get; set; }
    public required PlayerStatus PlayerStatus { get; set; }
    public required List<PersonalContactInfoDTO> PersonalContactInfos { get; set; }

}
