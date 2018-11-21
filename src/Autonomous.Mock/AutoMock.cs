namespace Autonomous.Mock
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Automatically Mock constructor arguments for a given type.
    /// </summary>
    public class AutoMock
    {
        /// <summary>
        /// Create a <see cref="Subject{TClass}"/>.
        /// </summary>
        /// <typeparam name="TClass">The type of TestSubject.</typeparam>
        /// <returns>The subject.</returns>
        public static ISubject<TClass> Create<TClass>()
            where TClass : class
        {
            Type type = typeof(TClass);
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = constructors.FirstOrDefault();

            var mockedArguments = GetMockedArguments(constructor);

            var concreteArguments = mockedArguments.Select(mock => ((Mock)mock).Object).ToArray();

            var instance = constructor.Invoke(concreteArguments) as TClass;

            return new Subject<TClass>(instance, mockedArguments);
        }

        /// <summary>
        /// Create a <see cref="Subject{TClass}"/>.
        /// </summary>
        /// <param name="configurationExpression">A configuration expression to setup rules for mocking a certain type.</param>
        /// <typeparam name="TClass">The type of test subject.</typeparam>
        /// <returns>The test subject.</returns>
        public static ISubject<TClass> Create<TClass>(Action<ConfigurationExpression> configurationExpression)
            where TClass : class
        {
            Type type = typeof(TClass);
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = constructors.FirstOrDefault();

            MockContext mockContext = null;
            if (configurationExpression != null)
            {
                mockContext = new MockContext();
                var expression = new ConfigurationExpression(mockContext.TypeMap);
                configurationExpression(expression);
            }

            var mockedArguments = GetMockedArguments(constructor, mockContext);

            var concreteArguments = mockedArguments.Select(implementation =>
            {
                if (!(implementation is Mock))
                {
                    return implementation;
                }

                return ((Mock)implementation).Object;
            })
            .ToArray();

            var instance = constructor.Invoke(concreteArguments) as TClass;

            return new Subject<TClass>(instance, mockedArguments);
        }

        private static IEnumerable<object> GetMockedArguments(ConstructorInfo constructor, IMockContext mockContext = null)
        {
            List<object> arguments = new List<object>();

            foreach (ParameterInfo argument in constructor.GetParameters())
            {
                if (mockContext != null && mockContext.TypeMap.Any(map => map.Key == argument.ParameterType))
                {
                    arguments.Add(mockContext.TypeMap.FirstOrDefault(map => map.Key == argument.ParameterType).Value);
                    continue;
                }

                Type mockType = typeof(Mock<>).MakeGenericType(argument.ParameterType);
                arguments.Add(Activator.CreateInstance(mockType));
            }

            return arguments;
        }
    }
}
