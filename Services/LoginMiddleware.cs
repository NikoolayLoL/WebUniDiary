
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using WebUniDiary.Logic;

namespace WebUniDiary.Services
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string INDEX = "/";

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var route = context.GetEndpoint()?.ToString();
            string controller = INDEX;
            string action = "Index";

            bool isLoggedIn = false;
            string userRole = "";

            if (route == null)
            {
                route = INDEX;
            }
            else
            {
                string[] paths = route.Split("/").ToArray();

                try
                {
                    controller = paths[1].ToLower();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    action = paths[2];
                }
                catch (Exception ex)
                {
                    action = "Index";
                }
            }

            // Check if Login cookie exists and retrieve its value
            if (context.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue1))
            {
                // We have a possibly logged in user, validate if session contains cookie.
                string storedUserCookie = context.Session.GetString(cookieValue1) ?? "";

                if (!string.IsNullOrEmpty(storedUserCookie))
                {
                    // Logged In, get Role
                    isLoggedIn = true;
                    userRole = storedUserCookie.ToLower();
                }
            }

            // Index page or Register
            if (route == INDEX || route == "/Index" || route == "/index" || route == "/Register")
            {
                if (isLoggedIn)
                {
                    context.Response.Redirect(CustomRedirect.RoleRedirect(userRole));
                    return;
                }

                await _next(context);
                return;
            }

            // Guest tries to hit a restricted Controller
            if (!isLoggedIn)
            {
                if (CustomRedirect.isAuthorizationNeeded(controller))
                {
                    context.Response.Redirect(INDEX);
                }

                // Otherwise Continue
            }
            else
            {
                if (!CustomRedirect.isUserAllowed(userRole, controller))
                {
                    context.Response.Redirect(CustomRedirect.RoleRedirect(userRole));
                }

                // Continue
            }

            await _next(context);
        }
    }
}
