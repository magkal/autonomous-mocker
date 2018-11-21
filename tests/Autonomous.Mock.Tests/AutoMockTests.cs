namespace Autonomous.Mock.Tests
{
	using Moq;
	using Xunit;

	public class AutoMockTests
	{
		[Fact(DisplayName = "AutoMock: Create should create an instance")]
		public void Create_create_an_instance()
		{
			var subject = AutoMock.Create<WebPage>();

			Assert.NotNull(subject.Instance);
		}

		[Fact(DisplayName = "AutoMock: Create should create dependencies")]
		public void Create_create_depdencies()
		{
			var subject = AutoMock.Create<WebPage>();

			Assert.NotNull(subject.Dependencies);
			Assert.NotEmpty(subject.Dependencies);
		}

		[Fact(DisplayName = "AutoMock: Should be able to verify call on dependency mock")]
		public void Should_be_able_to_verify_call_on_dependency_mock()
		{
			var subject = AutoMock.Create<WebPage>();
			var caddyMock = subject.GetMock<IDatabase>();

			subject.Instance.Render();

			caddyMock.Verify(caddy => caddy.GetData(), Times.Once);
		}

		[Fact(DisplayName = "AutoMock: Should be able to pass configuration expression to setup how to mock a specific type")]
		public void Should_be_able_to_pass_configuration_expression_to_setup_how_to_mock_certain_type()
		{
			Mock<IDatabase> mockedDatabase = new Mock<IDatabase>();
			mockedDatabase
				.Setup(caddy => caddy.GetData())
				.Returns("Mocked in-memory data");

			var subject = AutoMock.Create<WebPage>(config =>
			{
				config.For<IDatabase>(mockedDatabase);
			});

			string result = subject.Instance.Render();

			string expected = "Mocked in-memory data";

			Assert.Equal(expected, result);
		}

		[Fact(DisplayName = "AutoMock: Should be able to pass configuration expression to setup a concrete implementation")]
		public void Should_be_able_to_pass_configuration_expression_to_setup_a_concrete_implementation()
		{
			var subject = AutoMock.Create<WebPage>(config =>
			{
				config.For<IDatabase>(new Database());
			});

			string result = subject.Instance.Render();

			string expected = "Hello world data returned from a database.";

			Assert.Equal(expected, result);
		}
	}
}
