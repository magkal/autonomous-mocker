namespace Autonomous.Mock
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A context for the mocks, containing type maps and Mock configuration.
    /// </summary>
    internal class MockContext : IMockContext
    {
        /// <inheritdoc/>
        public Dictionary<Type, object> TypeMap { get; private set; } = new Dictionary<Type, object>();
    }
}
