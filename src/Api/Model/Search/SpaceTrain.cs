using Api.Model.SharedKernel;

namespace Api.Model.Search;

public record SpaceTrain(string Number, Bound Bound, string OriginId, string DestinationId, Schedule Schedule, List<Fare> Fares, List<string> CompatibleSpaceTrains);