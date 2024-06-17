using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipDotNetCore.RestApi.Services
{
    public class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AceInternship",
            UserID = "sa",
            Password = "twl@123",
            TrustServerCertificate = true
        };
    }
}
