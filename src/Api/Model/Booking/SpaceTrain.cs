using Api.Model.SharedKernel;

namespace Api.Model.Booking;

public record SpaceTrain(string Number, string OriginId, string DestinationId, Schedule Schedule, Fare Fare);