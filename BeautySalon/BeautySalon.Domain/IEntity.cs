namespace BeautySalon.Domain;

/// <summary>
/// Интерфейс для доменных сущностей с уникальным идентификатором
/// </summary>
public interface IEntity
{
	/// <summary>
	/// Уникальный идентификатор сущности
	/// </summary>
	int Id { get; set; }
}