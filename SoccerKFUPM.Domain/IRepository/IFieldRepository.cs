using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Domain.IRepository;

public interface IFieldRepository
{
    Task<List<Field>> GetAllFieldsAsync();
}