using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected SqlConnection sqlConnection;
        protected IDbTransaction dbTransaction;
        protected readonly string tableName;
        protected readonly string nameId;

        // For creating Update/Insert query
        private static IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();

        protected GenericRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction,
            string tableName)
        {
            this.sqlConnection = sqlConnection;
            this.dbTransaction = dbTransaction;
            this.tableName = tableName;
            this.nameId = typeof(TEntity).Name == "BookDetails" ? "BookDetailId" 
                : typeof(TEntity).Name == "User" ? "Id" : $"{typeof(TEntity).Name + "Id"}";
        }


        public virtual async Task<Guid> CreateAsync(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();

            entity.id = Guid.NewGuid();
            entity.DateCreated = DateTime.UtcNow;

            var result = await sqlConnection.ExecuteScalarAsync<Guid>(
                insertQuery,
                param: entity,
                transaction: dbTransaction);

            return result;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await sqlConnection.QueryAsync<TEntity>(
                $"SELECT * FROM [{tableName}]",
                transaction: dbTransaction);
        }
        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var retult =  await sqlConnection.QuerySingleOrDefaultAsync<TEntity>(
                $"SELECT * FROM [{tableName}] WHERE {nameId}=@Id",
                param: new { Id = id },
                transaction: dbTransaction);
            
            return retult;
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();

            await sqlConnection.ExecuteAsync(
                updateQuery,
                param: entity,
                transaction: dbTransaction);
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = GetByIdAsync(id).Result;
            if (entity == null) throw new Exception($"Entity [{id}] from [{tableName}] not found");

            entity.DateDeleted = DateTime.UtcNow;
            await UpdateAsync(entity);
            
            // await sqlConnection.ExecuteAsync(
            //     $"DELETE FROM [{tableName}] WHERE {nameId}=@Id",
            //     param: new { Id = id },
            //     transaction: dbTransaction);
        }


        // For update/insert query
        private List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            var list = listOfProperties
                .Select(prop => new
                {
                    attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false),
                    name = prop.Name
                })
                .Where(prop => 
                    prop.attributes.Length <= 0 || 
                    (prop.attributes[0] as DescriptionAttribute)?.Description != "ignore")
                .Select(prop => prop.name)
                .Where(propName => (nameId != "Id" && propName != "id") || (nameId == "id"))
                .ToList();
            return list;
        }
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder
                ($"DECLARE @InsertedIds TABLE (InsertId uniqueidentifier); INSERT INTO [{tableName}] ");

            insertQuery.Append('(');
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1) // remove last comma
                .Append($") OUTPUT inserted.{nameId} INTO @InsertedIds VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append("); SELECT InsertId FROM @InsertedIds;");

            return insertQuery.ToString();
        }
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE [{tableName}] SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals($"{nameId}"))
                    updateQuery.Append($"{property}=@{property},");
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); // remove last comma
            updateQuery.Append($" WHERE {nameId}=@{nameId}");

            return updateQuery.ToString();
        }
    }
}
