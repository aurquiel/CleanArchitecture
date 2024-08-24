using EnterpriseLayer;

namespace ApplicationLayer
{
    public class GetPostUseCase
    {
        private readonly IExternalServiceAdapter<Post> _adapter;

        public GetPostUseCase(IExternalServiceAdapter<Post> adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<Post>> ExecuteAsync()
        {
            return await _adapter.GetDataAsync();
        }
    }
}
