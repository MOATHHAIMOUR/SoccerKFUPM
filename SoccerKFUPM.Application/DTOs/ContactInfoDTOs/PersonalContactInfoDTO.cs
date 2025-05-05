namespace SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Domain.Entities.Enums;

public class PersonalContactInfoDTO
{
    public ContactType ContactType { get; set; }
    public required string Value { get; set; }
}