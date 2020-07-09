using System;
using Champlin.Common;

namespace Champlin.DataLayer
{
    public static class CosmosHelpers
    {
        public static string GetType<T>() where T : BaseEntity
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            return instance.Type;
        }
    }
}