namespace Autonomous.Mock.Tests
{
	public class Database : IDatabase
	{
		public string GetData()
		{
			return "Hello world data returned from a database.";
		}
	}
}
