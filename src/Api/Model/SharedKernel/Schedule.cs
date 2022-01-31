namespace Api.Model.SharedKernel;

public record Schedule
{
    public DateTime Departure { get; }
    public DateTime Arrival { get; }
    public TimeSpan Duration { get; }

    public Schedule(DateTime departure, DateTime arrival)
    {
        Departure = departure;
        Arrival = arrival;
        Duration = Arrival - Departure;
    }
}
