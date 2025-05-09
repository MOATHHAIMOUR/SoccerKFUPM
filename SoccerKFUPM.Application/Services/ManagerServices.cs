using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.Helpers;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace SoccerKFUPM.Application.Services;

public class ManagerServices : IManagerServices
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;
    private UserManager<User> _userManager;

    public ManagerServices(IManagerRepository managerRepository, IMapper mapper, UserManager<User> userManager)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Result<bool>> AddManagerAsync(Manager manager, string username, string IntialPassword)
    {
        int? personId = await _managerRepository.AddManagerAsync(manager);

        if (personId == null)
            Result<bool>.Failure(Error.ValidationError("Failed to create Coach."), HttpStatusCode.BadRequest);

        var IsManagerCreated = await AuthHelpers.CreateUserWithRoleAsync(_userManager, personId.Value, username, IntialPassword, "Manager");

        return IsManagerCreated.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.ValidationError("Failed to create user for Manager."), HttpStatusCode.BadRequest);

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