using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public interface IClientService : ICrudService<ClientDto, ClientEditDto>;