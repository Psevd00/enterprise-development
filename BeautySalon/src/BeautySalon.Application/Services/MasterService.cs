using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления мастерами
/// </summary>
public class MasterService(IMasterRepository repository)
    : CrudService<Master, MasterDto, MasterEditDto>(repository), IMasterService
{
    protected override MasterDto ToDto(Master entity) => entity.ToDto();

    protected override Task<Master> BuildAsync(MasterEditDto dto, CancellationToken cancellationToken) =>
        Task.FromResult(new Master
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            WorkExperience = dto.WorkExperience
        });

    protected override Task ApplyAsync(Master entity, MasterEditDto dto, CancellationToken cancellationToken)
    {
        entity.FullName = dto.FullName;
        entity.Gender = dto.Gender;
        entity.PassportNumber = dto.PassportNumber;
        entity.Specialization = dto.Specialization;
        entity.WorkExperience = dto.WorkExperience;
        return Task.CompletedTask;
    }
}