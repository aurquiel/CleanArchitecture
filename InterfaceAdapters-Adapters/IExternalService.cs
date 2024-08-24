namespace InterfaceAdapters_Adapters
{
    public interface IExternalService<T>
    {
        public Task<IEnumerable<T>> GetContentAsync();
    }
}
