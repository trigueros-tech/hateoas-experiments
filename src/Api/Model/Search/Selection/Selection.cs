using Api.Model.SharedKernel;

namespace Api.Model.Search.Selection;

public record Selection(Dictionary<Bound, SelectedSpaceTrain> SelectedSpaceTrainsByBound)
{
    public decimal TotalPrice => this.SelectedSpaceTrainsByBound.Sum(x => x.Value.Price);
}