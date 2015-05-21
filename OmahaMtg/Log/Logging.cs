using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace OmahaMtg.Log
{
    public static class Logging
    {
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

        public static void Setup()
        {
            SetupDb();

            Serilog.Log.Logger = new LoggerConfiguration()
             //.WriteTo.MSSqlServer("DefaultConnection", "Logs")
             .WriteTo.MSSqlServer(ConnectionString, "Logs")
                .CreateLogger();
        }

        private static void SetupDb()
        {
            if (!DoesLogTableExist())
            {
                using (SqlConnection conn =
                    new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand command =
                         new SqlCommand(@"CREATE TABLE [Logs](
	                                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                                    [Message] [nvarchar](max) NULL,
	                                    [MessageTemplate] [nvarchar](max) NULL,
	                                    [Level] [nvarchar](128) NULL,
	                                    [TimeStamp] [datetime] NOT NULL,
	                                    [Exception] [nvarchar](max) NULL,
	                                    [Properties] [xml] NULL,
                                     CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
                                    (
	                                    [Id] ASC
                                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]",conn);

                    command.ExecuteNonQuery();

                }

            }
        }

        private static bool DoesLogTableExist()
        {
            using (SqlConnection conn =
                     new SqlConnection(ConnectionString))
            {
                conn.Open();

                DataTable dTable = conn.GetSchema("TABLES",
                               new string[] { null, null, "Logs" });

                if (dTable.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }


        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Serilog.Log.Information(messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Serilog.Log.Error(messageTemplate, propertyValues);
        }

        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Serilog.Log.Error(exception, messageTemplate, propertyValues);
        }
    }
}
