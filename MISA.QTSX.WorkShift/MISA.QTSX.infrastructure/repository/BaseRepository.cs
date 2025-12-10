using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.QTSX.CORE.interfaces.repository;
using System.Data;
using MISA.QTSX.CORE.misaatribiute;
using System.Reflection;

namespace MISA.QTSX.infrastructure.repository
{
    public class BaseRepository<T> : IBaseRepository<T>,IDisposable
    {
        // Change the field declaration to nullable to fix CS8618
        protected readonly string? connectionString;
        protected IDbConnection connection;
        public BaseRepository   (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MySQLConnection");
            connection = new MySqlConnection(connectionString);
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu từ bảng
        /// </summary>
        /// <returns>Danh sách tất cả các bản ghi</returns>
        public List<T> GetAll() { 
            // lấy ra  tableName
            var tableName = typeof(T).Name;
            // câu lệnh truy vấn
            var sqlCommand = $"SELECT * FROM {tableName}";
            var data = connection.Query<T>(sqlCommand);
            return data.ToList();
        }
        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi tương ứng với ID, null nếu không tìm thấy</returns>
        public T Get(Guid id)
        {
            //Lấy ra tableName
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            // Lấy ra tableNameId
            var primaryKey = properties.FirstOrDefault(col => col.Name.Equals($"{tableName}Id")).GetCustomAttribute<MisaColumnName>().Name;

            var sqlCommand = $"SELECT * FROM {tableName} WHERE {primaryKey} = @id";
            var data = connection.Query<T>(sqlCommand, new { id });
            return data.FirstOrDefault();
        }
        /// <summary>
        /// Thêm mới một bản ghi vào bảng
        /// </summary>
        /// <param name="entity">Đối tượng chứa dữ liệu cần thêm mới</param>
        /// <returns>Đối tượng sau khi được thêm mới (có ID được sinh tự động)</returns>
        public T Insert(T entity) {     
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var columns = "";
            var columnParam = "";
            var parameters = new DynamicParameters();
            // sinh Id 
            Guid newGuid = Guid.NewGuid();
            // tìm tableNameId và set giá trị guid
            var columnId = properties.FirstOrDefault(col => col.Name.Equals($"{tableName}Id"));
            columnId?.SetValue(entity, newGuid);
           // nối các cột của table , pramater
            foreach (var property in properties)
            {
                var columnName = property.GetCustomAttributes<MisaColumnName>().FirstOrDefault().Name;
                columns += $"{columnName},";
                columnParam += $"@{columnName},";
                parameters.Add(columnName, property.GetValue(entity));
            }
            columns = columns.TrimEnd(',');
            columnParam = columnParam.TrimEnd(',');
            var sqlCommand = $"INSERT INTO {tableName} ({columns}) VALUES ({columnParam})";
            connection.Execute(sqlCommand, parameters);
            return entity;
        }
        /// <summary>
        /// Cập nhật dữ liệu của một bản ghi
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Đối tượng chứa dữ liệu mới để cập nhật</param>
        /// <returns>Đối tượng sau khi được cập nhật</returns>
        public T Update(Guid id, T entity)
        {   
            var tableName = typeof(T).Name;          
            var properties = typeof(T).GetProperties();
            var primaryKeyProperty = properties.FirstOrDefault(col => col.Name.Equals($"{tableName}Id"));
            var primaryKey = primaryKeyProperty?.GetCustomAttribute<MisaColumnName>().Name;
            var setClause = "";              
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                // Bỏ qua primary key - không update primary key
                if (property.Name.Equals($"{tableName}Id"))
                {
                    continue;
                }
                
                var columnName = property.GetCustomAttributes<MisaColumnName>().FirstOrDefault()?.Name;        
                setClause += $"{columnName} = @{property.Name},";
                parameters.Add(property.Name, property.GetValue(entity));
            }

            setClause = setClause.TrimEnd(',');
            parameters.Add("id", id);
            var sqlCommand = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @id";
            connection.Execute(sqlCommand, parameters);
            return entity;
        }
        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không tìm thấy bản ghi</returns>
        public bool Delete(Guid id) { 
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var primaryKey = properties.Where(p=>p.Name.Equals("ShiftId")).Select(p=>p.GetCustomAttribute<MisaColumnName>()?.Name).FirstOrDefault();
         
            var sqlCommand = $"DELETE FROM {tableName} WHERE {primaryKey} = @id";
            var data = connection.Execute(sqlCommand, new { id });
            return Convert.ToInt32(data) > 0;
        }
        public void Dispose() { 
            connection?.Dispose();
        }

    }
}
