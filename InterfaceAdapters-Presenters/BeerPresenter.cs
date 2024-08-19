using ApplicationLayer;
using EnterpriseLayer;

namespace InterfaceAdapters_Presenters
{
    public class BeerPresenter : IPresenter<Beer, BeerViewModel>
    {
        public IEnumerable<BeerViewModel> Present(IEnumerable<Beer> data)
        {
            return data.Select(b => new BeerViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol.ToString() + "%",
            }).ToList();
        }
    }
}
