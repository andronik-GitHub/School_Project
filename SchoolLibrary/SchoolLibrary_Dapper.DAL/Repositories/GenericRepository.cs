using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected SqlConnection sqlConnection;
        protected IDbTransaction dbTransaction;
        protected readonly string tableName;

        // For creating Update/Insert query
        private static IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();

        public GenericRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction,
            string tableName)
        {
            this.sqlConnection = sqlConnection;
            this.dbTransaction = dbTransaction;
            this.tableName = tableName;
        }


        public virtual async Task<Guid> CreateAsync(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();

            return await sqlConnection.ExecuteScalarAsync<Guid>(
                insertQuery,
                param: entity,
                transaction: dbTransaction);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await sqlConnection.QueryAsync<TEntity>(
                $"SELECT * FROM [{tableName}]",
                transaction: dbTransaction);
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            string nameId = $"{typeof(TEntity).Name + "Id"}";

            var retult =  await sqlConnection.QuerySingleOrDefaultAsync<TEntity>(
                $"SELECT * FROM [{tableName}] WHERE {nameId}=@Id",
                param: new { Id = id },
                transaction: dbTransaction);

            if (retult == null) throw new Exception($"Error in: public virtual async Task<{typeof(TEntity).Name}> GetAsync(int id)");
            else return retult;
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();

            await sqlConnection.ExecuteAsync(
                updateQuery,
                param: entity,
                transaction: dbTransaction);
        }
        public async Task DeleteAsync(Guid id)
        {
            string nameId = $"{typeof(TEntity).Name + "Id"}";

            await sqlConnection.ExecuteAsync(
                $"DELETE FROM [{tableName}] WHERE {nameId}=@Id",
                param: new { Id = id },
                transaction: dbTransaction);
        }


        // For update/insert query
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            insertQuery.Append('(');

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append("); SELECT SCOPE_IDENTITY()");

            return insertQuery.ToString();
        }
        private string GenerateUpdateQuery()
        {
            // PK tables Users => UserId
            string nameId = $"{typeof(TEntity).Name + "Id"}";

            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals($"{nameId}"))
                    updateQuery.Append($"{property}=@{property},");
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append($" WHERE {nameId}=@{nameId}");

            return updateQuery.ToString();
        }
    }
}
