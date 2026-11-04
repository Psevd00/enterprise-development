using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления услугами салона
/// </summary>
public interface IServiceService : ICrudService<ServiceDto, ServiceEditDto>;