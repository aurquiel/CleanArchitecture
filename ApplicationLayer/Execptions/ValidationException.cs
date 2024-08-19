namespace ApplicationLayer.Execptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Erro Validacion.") 
        {
        }

        public ValidationException(string error) : base(error) 
        {
            
        }
    }
}
