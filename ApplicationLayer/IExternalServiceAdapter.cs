namespace ApplicationLayer
{
    public interface IExternalServiceAdapter<TEntity>
    {
        Task<IEnumerable<TEntity>> GetDataAsync();
    }
}
