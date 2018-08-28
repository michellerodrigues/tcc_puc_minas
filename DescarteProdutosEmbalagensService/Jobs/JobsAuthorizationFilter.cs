using Hangfire.Dashboard;

namespace DescarteServices.Jobs
{
    public class JobsAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }    
    }
}