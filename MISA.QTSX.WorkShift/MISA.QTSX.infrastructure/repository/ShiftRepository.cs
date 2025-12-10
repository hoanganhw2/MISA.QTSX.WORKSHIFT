using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.QTSX.CORE.entities;
using MISA.QTSX.CORE.interfaces.repository;
using MISA.QTSX.CORE.misaatribiute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.infrastructure.repository
{
    public class ShiftRepository :  BaseRepository<Shift>,IShiftRepository
    {
        public ShiftRepository(IConfiguration configuration) : base(configuration) { }

        public bool CheckDuplicate(Shift shift)
        {
            var tableName = typeof(Shift).Name.ToLower();
            var properties = typeof(Shift).GetProperties();
            // lấy ra tên column tương ứng với ShiftCode trong CSDL
            var columnName = properties
            .Where(p => p.Name == "ShiftCode")
            .Select(p => p.GetCustomAttribute<MisaColumnName>()?.Name)
            .FirstOrDefault();
            var sqlCommand = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @ShiftCode";
            var count = connection.QueryFirstOrDefault<int>(sqlCommand, new { shift.ShiftCode });
            return count > 0;
        }
    }
}
