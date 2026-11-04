using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления мастерами
/// </summary>
public interface IMasterService : ICrudService<MasterDto, MasterEditDto>;