namespace BeautySalon.Domain;

public class Client : Person {
    public required string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
}