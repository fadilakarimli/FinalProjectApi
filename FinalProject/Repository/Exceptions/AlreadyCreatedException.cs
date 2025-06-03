namespace Repository.Exceptions
{
    public class AlreadyCreatedException : Exception
    {
        public AlreadyCreatedException(string message) : base(message) { }
    }
}
