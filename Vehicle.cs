public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Colour { get; set; }
    public string Type { get; set; }
    public DateTime CheckInTime { get; set; }

    public Vehicle(string registrationNumber, string colour, string type)
    {
        RegistrationNumber = registrationNumber;
        Colour = colour;
        Type = type;
        CheckInTime = DateTime.Now;
    }
}