using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.misaatribiute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MisaRequired :Attribute
    {
        public MisaRequired(string errorMessage)
        {
            
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MisaCheckDuplicate:Attribute
    {
        public MisaCheckDuplicate(string errorMessage)
        {
            
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MisaColumnName : Attribute
    {
       public string Name { get; }
        public MisaColumnName(string columnName)
        {
            Name = columnName;
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class MisaTable:Attribute
    {
        public string Name { get; }
        public MisaTable(string tableName)
        {
            Name = tableName;
        }
    }
    
}
