namespace BeautySalon.Domain;

public class Appointment { 
    public required Master Master { get; set; }
    public required Client Client { get; set; }
    public required Service Service { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsRegularClient { get; set; }
}