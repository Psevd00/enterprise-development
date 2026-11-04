using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Exceptions;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления записями на приём
/// </summary>
public class AppointmentService(
    IAppointmentRepository repository,
    IClientRepository clientRepository,
    IMasterRepository masterRepository,
    IServiceRepository serviceRepository)
    : CrudService<Appointment, AppointmentDto, AppointmentEditDto>(repository), IAppointmentService
{
    protected override AppointmentDto ToDto(Appointment entity) => entity.ToDto();

    protected override async Task<Appointment> BuildAsync(AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        var (client, master, service) = await ResolveReferencesAsync(dto, cancellationToken);

        return new Appointment
        {
            Client = client,
            Master = master,
            Service = service,
            DateTime = dto.DateTime,
            IsRegularClient = dto.IsRegularClient
        };
    }

    protected override async Task ApplyAsync(Appointment entity, AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        var (client, master, service) = await ResolveReferencesAsync(dto, cancellationToken);

        entity.Client = client;
        entity.Master = master;
        entity.Service = service;
        entity.DateTime = dto.DateTime;
        entity.IsRegularClient = dto.IsRegularClient;
    }

    private async Task<(Client Client, Master Master, Service Service)> ResolveReferencesAsync(
        AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId, cancellationToken)
            ?? throw new ReferenceNotFoundException(nameof(Client), dto.ClientId);
        var master = await masterRepository.GetByIdAsync(dto.MasterId, cancellationToken)
            ?? throw new ReferenceNotFoundException(nameof(Master), dto.MasterId);
        var service = await serviceRepository.GetByIdAsync(dto.ServiceId, cancellationToken)
            ?? throw new ReferenceNotFoundException(nameof(Service), dto.ServiceId);

        return (client, master, service);
    }
}