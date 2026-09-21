namespace BeautySalon.Domain;

public abstract class Person { 
    public required string FullName { get; set; }
    public Gender Gender { get; set; }
    
}