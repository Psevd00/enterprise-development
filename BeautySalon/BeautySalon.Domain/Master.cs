namespace BeautySalon.Domain;

public class Master : Person 
{ 
    public required string PassportNumber { get; set; }
    public required Specialization Specialization { get; set; }
    public int WorkExperience { get; set; }
}