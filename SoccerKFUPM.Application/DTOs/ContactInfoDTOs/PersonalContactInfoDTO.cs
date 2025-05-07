namespace SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Domain.Entities.Enums;

public class PersonalContactInfoDTO
{
    public int ContactType { get; set; }
    public required string Value { get; set; }
}