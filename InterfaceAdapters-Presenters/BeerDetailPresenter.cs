using ApplicationLayer;
using EnterpriseLayer;

namespace InterfaceAdapters_Presenters
{
    public class BeerDetailPresenter : IPresenter<Beer, BeerDetailViewModel>
    {
        public IEnumerable<BeerDetailViewModel> Present(IEnumerable<Beer> beers)
            => beers.Select(b => new BeerDetailViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol + "%",
                Color = b.IsStrongBeer() ? "red" : "green",
                Style = b.Style,
                Message = b.IsStrongBeer() ? "Cerveza fuerte" : ""
            });
    }
}
