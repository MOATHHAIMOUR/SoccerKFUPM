using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;

namespace SoccerKFUPM.Application.Services;

public class ManagerServices : IManagerServices
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;

    public ManagerServices(IManagerRepository managerRepository, IMapper mapper)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> AddManagerAsync(Manager manager)
    {

        await _managerRepository.AddManagerAsync(manager);

        return Result<bool>.Success(true);

    }

    public async Task<Result<ManagerDTO>> GetManagerByIdAsync(int managerId)
    {

        var manager = await _managerRepository.GetManagerByIdAsync(managerId);
        if (manager is null)
            return Result<ManagerDTO>.Failure(Error.RecoredNotFound($"manager with id: {managerId} is not found"), System.Net.HttpStatusCode.NotFound);

        var managerDto = _mapper.Map<ManagerDTO>(manager);
        return Result<ManagerDTO>.Success(managerDto);


    }


    public async Task<Result<(List<ManagerView> Managers, int TotalCount)>> SearchManagersAsync(
    string? kfupmId,
    string? firstName,
    string? secondName,
    string? thirdName,
    string? lastName,
    DateTime? dateOfBirth,
    int? nationalityId,
    string? teamName,
    int pageNumber,
    int pageSize)
    {
        var (managers, totalCount) = await _managerRepository.SearchManagersAsync(
            kfupmId,
            firstName,
            secondName,
            thirdName,
            lastName,
            dateOfBirth,
            nationalityId,
            teamName,
            pageNumber,
            pageSize
        );

        return Result<(List<ManagerView>, int)>.Success((managers, totalCount));
    }

}