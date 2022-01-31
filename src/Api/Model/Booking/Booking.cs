namespace Api.Model.Booking;

public record Booking(Guid Id,  List<SpaceTrain> SpaceTrains)
{
    public decimal TotalPrice => SpaceTrains.Sum(x => x.Fare.Price);
}