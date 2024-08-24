using ApplicationLayer;
using EnterpriseLayer;
using InterfaceAdapters_Adapters.Dtos;

namespace InterfaceAdapters_Adapters
{
    public class PostExternalServiceAdapter : IExternalServiceAdapter<Post>
    {
        private readonly IExternalService<PostServiceDTO> _externalService;

        public PostExternalServiceAdapter(IExternalService<PostServiceDTO> externalService)
        {
            _externalService = externalService;
        }

        public async Task<IEnumerable<Post>> GetDataAsync()
        {
            var postsES = await _externalService.GetContentAsync();
            var post = postsES.Select(p => new Post { 
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
            });

            return post;    
        }
    }
}
