using ApplicationLayer;
using EnterpriseLayer;
using InterfaceAdapters_Mappers.Dtos.Request;

namespace InterfaceAdapters_Mappers
{
    public class BeerMapper : IMapper<BeerRequestDTO, Beer>
    {
        public Beer ToEntity(BeerRequestDTO dto) => new Beer
        {
            Id = dto.Id,
            Name = dto.Name,
            Style = dto.Style,
            Alcohol = dto.Alcohol,
        };

    }
}
