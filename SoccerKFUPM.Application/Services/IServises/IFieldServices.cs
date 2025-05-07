using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.SharedDTOs;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IFieldServices
{
    Task<Result<List<FieldDTO>>> GetAllFieldsAsync();
}