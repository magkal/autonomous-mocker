namespace Autonomous.Mock
{
    using Moq;
    using System.Collections.Generic;

    /// <summary>
    /// An abstraction of the testable subject.
    /// </summary>
    /// <typeparam name="TClass">The type of class to test.</typeparam>
    public interface ISubject<TClass>
        where TClass : class
    {
        /// <summary>
        /// Gets the concrete test subject instance.
        /// </summary>
        TClass Instance { get; }

        /// <summary>
        /// Gets an enumerable representation of the subject constructor dependencies.
        /// </summary>
        IEnumerable<object> Dependencies { get; }

        /// <summary>
        /// Will resolve the requested <see cref="Mock{T}"/> from <see cref="Dependencies"/>.
        /// </summary>
        /// <typeparam name="T">The type of Mocked object to look for.</typeparam>
        /// <returns>The mocked representation of the object.</returns>
        Mock<T> GetMock<T>()
           where T : class;
    }
}