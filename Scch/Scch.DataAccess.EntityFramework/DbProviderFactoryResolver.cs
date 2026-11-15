using System.Data.Common;
using System.Data.SqlClient;

namespace Scch.DataAccess.EntityFramework
{
    /// <summary>
    /// Resolves <see cref="DbProviderFactory"/> instances by their provider invariant name.
    /// </summary>
    internal static class DbProviderFactoryResolver
    {
        private const string SqlClientInvariantName = "System.Data.SqlClient";

        static DbProviderFactoryResolver()
        {
            // The .NET Framework registered the SqlClient provider machine-wide (machine.config).
            // .NET has no such registration, so it is added here unless the application already did.
            if (!DbProviderFactories.TryGetFactory(SqlClientInvariantName, out _))
            {
#pragma warning disable CS0618 // System.Data.SqlClient is the provider used by the EF6 SQL Server provider
                DbProviderFactories.RegisterFactory(SqlClientInvariantName, SqlClientFactory.Instance);
#pragma warning restore CS0618
            }
        }

        /// <summary>
        /// Returns the <see cref="DbProviderFactory"/> for the specified provider invariant name.
        /// </summary>
        /// <param name="providerInvariantName">The provider invariant name, e.g. from a connection string setting.</param>
        /// <returns>The <see cref="DbProviderFactory"/>.</returns>
        public static DbProviderFactory GetFactory(string providerInvariantName)
        {
            return DbProviderFactories.GetFactory(providerInvariantName);
        }
    }
}
