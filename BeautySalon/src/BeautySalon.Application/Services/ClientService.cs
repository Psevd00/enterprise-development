using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public class ClientService(IClientRepository repository)
    : CrudService<Client, ClientDto, ClientEditDto>(repository), IClientService
{
    protected override ClientDto ToDto(Client entity) => entity.ToDto();

    protected override Task<Client> BuildAsync(ClientEditDto dto, CancellationToken cancellationToken) =>
        Task.FromResult(new Client
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PhoneNumber = dto.PhoneNumber
        });

    protected override Task ApplyAsync(Client entity, ClientEditDto dto, CancellationToken cancellationToken)
    {
        entity.FullName = dto.FullName;
        entity.Gender = dto.Gender;
        entity.BirthDate = dto.BirthDate;
        entity.PhoneNumber = dto.PhoneNumber;
        return Task.CompletedTask;
    }
}