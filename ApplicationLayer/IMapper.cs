namespace ApplicationLayer
{
    public interface IMapper<TDTO,TOutput>
    {
        public TOutput ToEntity(TDTO dto);
    }
}
