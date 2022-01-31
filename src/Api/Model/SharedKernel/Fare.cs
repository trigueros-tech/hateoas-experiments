namespace Api.Model.SharedKernel;

public record Fare(Guid Id, ComfortClass ComfortClass, decimal Price);