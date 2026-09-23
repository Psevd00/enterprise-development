namespace BeautySalon.Domain;

public class Service { 
    public required string Name { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
}