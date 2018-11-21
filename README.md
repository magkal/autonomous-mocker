# 🤖 Autonomous Mocker

A **very** _lightweight_ extension the popular C# Moq library, to be able automatically setup your test subjects. Allowing you to write tests faster with less code and more readability. Automatically mocking the parameters if a certain class's constructor parameters.


## Todo list
- [x] Auto mock constructor parameters
- [x] Be able to pass in configuration arguments to setup mocks and concrete implementations
- [ ] Be able to configure the Moq.Mock Behavior

## 🎬 Getting started ##
#### Examples ####
Some examples with the imaginary Golfer class.

```csharp
[Fact]
public void Golfer_should_swing_with_given_club()
{
	var subject = AutoMock.Create<Golfer>();

	var result = subject.Instance.Swing(); 

	Assert.Equal("I swung a mocked club", result);
}

[Fact]
public void Golfer_should_use_concrete_caddy()
{
	var subject = AutoMock.Create<Golfer>(config => 
	{
		config.For<ICaddy>(new Caddy());
	});

	var result = subject.Instance.Swing();

	Assert.Equal("I swung a real club", result);
}
```