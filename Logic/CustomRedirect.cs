namespace WebUniDiary.Logic
{
    public static class CustomRedirect
    {
        public static string RoleRedirect(string role)
        {
            switch (role.ToLower())
            {
                case "admin":
                    return "/Admin";
                case "student":
                    return "/Student/Index";
                case "teacher":
                    return "/Teacher/Index";
                default:
                    return "/Error1";
            }
        }

        public static bool isAuthorizationNeeded(string route)
        {
            switch (route.ToLower())
            { 
                case "admin":
                case "student":
                case "teacher":
                    return true;
                default:
                    return false;
            }
        }

        public static bool isUserAllowed(string role, string route)
        {
            if (isAuthorizationNeeded(route.ToLower()))
            {
                if (role != route)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
