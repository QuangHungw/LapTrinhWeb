using SV20T1020042.DomainModels;
using SV20T1020042.DataLayers.SQLServer;
using SV20T1020042.DataLayers;
namespace SV20T1020042.BusinessLayers
{
    public static class UserAccountService
    {
        private static readonly IUserAccountDAL employeeAccountDB;

        static UserAccountService()
        {
            employeeAccountDB = new EmployeeAccountDAL(Configuration.ConnectionString);
        }

        public static UserAccount? Authorize(string userName, string password)
        {
            return employeeAccountDB.Authorize(userName, password);
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }
        public static string? GetPassword(string userName)
        {
            return employeeAccountDB.GetPassword(userName);
        }

    }
}