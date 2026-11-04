using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления услугами салона
/// </summary>
public class SalonServiceService(IServiceRepository repository)
    : CrudService<Service, ServiceDto, ServiceEditDto>(repository), IServiceService
{
    protected override ServiceDto ToDto(Service entity) => entity.ToDto();

    protected override Task<Service> BuildAsync(ServiceEditDto dto, CancellationToken cancellationToken) =>
        Task.FromResult(new Service
        {
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Duration = dto.Duration
        });

    protected override Task ApplyAsync(Service entity, ServiceEditDto dto, CancellationToken cancellationToken)
    {
        entity.Name = dto.Name;
        entity.Category = dto.Category;
        entity.Price = dto.Price;
        entity.Duration = dto.Duration;
        return Task.CompletedTask;
    }
}