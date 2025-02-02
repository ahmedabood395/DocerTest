namespace _4EPlatform_FAQ.GrpClientsConfig
{

    public static class GrpcClients
    {
        #region UserManagement
        public static void AddUserManagementGrpcClients(
            this IServiceCollection services,
            string userManagmentUrl)
        {
            services.AddGrpcServiceClient<PermissionService.PermissionServiceClient>(userManagmentUrl);
            services.AddGrpcServiceClient<PermissionGroupingService.PermissionGroupingServiceClient>(userManagmentUrl);
        }
        #endregion

    }
}
