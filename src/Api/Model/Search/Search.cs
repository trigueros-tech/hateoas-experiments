namespace Api.Model.Search;

public record Search(Guid Id, Criteria.Criteria Criteria, List<SpaceTrain> SpaceTrains, Selection.Selection Selection);