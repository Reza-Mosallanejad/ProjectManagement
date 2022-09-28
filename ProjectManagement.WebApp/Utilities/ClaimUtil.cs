using System.Security.Claims;

namespace ProjectManagement.WebApp.Utilities
{
    public static class ClaimUtil
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userId = 0;
            int.TryParse(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "0", out userId);
            return userId;
        }
    }
}
