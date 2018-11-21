namespace Autonomous.Mock
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contextual settings for the context in which the Mocks are done.
    /// </summary>
    internal interface IMockContext
    {
        /// <summary>
        /// Gets the type map.
        /// </summary>
        Dictionary<Type, object> TypeMap { get; }
    }
}