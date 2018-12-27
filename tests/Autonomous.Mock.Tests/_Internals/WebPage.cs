namespace Autonomous.Mock.Tests
{
	public class WebPage : IPage
	{
		private readonly IDatabase _database;
		private readonly IWebService _webService;

		public WebPage(IDatabase database)
			: this(database, null)
		{
		}

		public WebPage(IDatabase database, IWebService webService)
		{
			_database = database;
			_webService = webService;
		}

		public string Render()
		{
			return $"{_database.GetData()}{_webService.GetData()}";
		}
	}
}
