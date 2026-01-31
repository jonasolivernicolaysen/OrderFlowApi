namespace OrderFlowApi.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException() 
            : base($"You are not authorized to access this resource.") 
        { 
        }
    }
}
