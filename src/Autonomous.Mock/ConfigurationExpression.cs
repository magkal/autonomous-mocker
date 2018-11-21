namespace Autonomous.Mock
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A set of methods to configure how a type should be mapped to an instance.
    /// </summary>
    public class ConfigurationExpression
    {
        private readonly Dictionary<Type, object> _typeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExpression"/> class.
        /// </summary>
        /// <param name="mockContextTypeMap">The type map reference to a <see cref="IMockContext"/>.</param>
        public ConfigurationExpression(Dictionary<Type, object> mockContextTypeMap)
        {
            _typeMap = mockContextTypeMap;
        }

        /// <summary>
        /// Set how to map a type to a specific object.
        /// </summary>
        /// <typeparam name="TClass">The type of class to use as key.</typeparam>
        /// <param name="useObject">The type of object that should be returned for the given type.</param>
        public void For<TClass>(object useObject)
        {
            _typeMap.Add(typeof(TClass), useObject);
        }
    }
}
