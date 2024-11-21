namespace RestApi.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId) 
            : base($"User with ID {userId} does not exist.")
        {
        }
    }
}