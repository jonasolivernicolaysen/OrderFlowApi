namespace OrderFlowApi.User
{
    public class FakeUserLogic
    {
        public static int GetCurrentUserId(bool isAdmin = false)
        {
            if (isAdmin)
            {
                return 0;
            }
            return 1;
        }
    }
}
