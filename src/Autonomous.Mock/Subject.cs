namespace Autonomous.Mock
{
    using Moq;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a test subject with the testable instance and it's constructor dependencies.
    /// </summary>
    /// <typeparam name="TClass">The type of class to test.</typeparam>
    public class Subject<TClass> : ISubject<TClass>
        where TClass : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subject{TClass}"/> class.
        /// </summary>
        /// <param name="instance">The instance to test.</param>
        /// <param name="dependencies">The constructor dependencies.</param>
        public Subject(TClass instance, IEnumerable<object> dependencies)
        {
            Instance = instance;
            Dependencies = dependencies;
        }

        /// <inheritdoc/>
        public TClass Instance { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<object> Dependencies { get; private set; }

        /// <inheritdoc/>
        public Mock<T> GetMock<T>()
            where T : class
        {
            return Dependencies.First(dependency => dependency is Mock<T>) as Mock<T>;
        }
    }
}
