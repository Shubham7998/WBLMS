using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Repositories;
using WBLMS.Services;

namespace WBLMS.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMyServicesLeaveRequest(this IServiceCollection services)
        {
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

            services.AddScoped<AuthService>();

            

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<AuthService>();
            return services;
        }
        public static IServiceCollection AddMyServicesEmployee(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
