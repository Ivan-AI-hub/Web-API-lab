using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Whosales.Persistence
{
	public class DataBaseConnection
	{
		private DbConnection _connection;
		private static DataBaseConnection _instance;

		public static DataBaseConnection Instance
		{
			get
			{
				return _instance;
			}
		}
		static DataBaseConnection()
		{
			_instance = new DataBaseConnection();
		}

		private DataBaseConnection()
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");

			var config = builder.Build();
			string connectionString = config.GetConnectionString("MainConnection");
			_connection = new SqlConnection(connectionString);
		}

		public DbConnection GetConnection()
		{
			return _connection;
		}
	}
}
