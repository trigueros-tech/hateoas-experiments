namespace Api.Model.Search.Criteria;

public record Journey(string DepartureSpacePortId, DateTime DepartureSchedule, string ArrivalSpacePortId)
{
    public bool IsConnectedTo(Journey journey) => journey.DepartureSpacePortId == ArrivalSpacePortId;
}