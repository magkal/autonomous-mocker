namespace Autonomous.Mock.Tests
{
	public class WebPage : IPage
	{
		private readonly IDatabase _database;

		public WebPage(IDatabase database)
		{
			_database = database;
		}

		public string Render()
		{
			return _database.GetData();
		}
	}
}
