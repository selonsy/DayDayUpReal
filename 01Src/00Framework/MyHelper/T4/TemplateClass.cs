﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devin
{

    /// <summary>
    /// 配置基类
    /// </summary>
    public abstract class BaseConfig
    {
        public abstract string ConnectionString { get; }
        public abstract string DbDataBase { get; }
        public abstract string TableName { get; }
        public abstract string[] NoExistFields { get; }
    }

    /// <summary>
    /// 配置类
    /// </summary>
    public class TConfig : BaseConfig
    {
        public override string ConnectionString
        {
            get
            {
                return @"server=.;uid=sa;pwd=95938;database=Test";                
            }
        }
        public override string DbDataBase
        {
            get { return "Test"; }
        }

        public override string TableName
        {
            get { return "TestTable"; }
        }

        public override string[] NoExistFields
        {
            get { return new string[] { }; }
        }

    }

    /// <summary>
    /// DBHelper类
    /// </summary>
    public class DbHelper
    {
        public BaseConfig Config { get; private set; }
        public List<DbColumn> DbColumnList { get; private set; }
        public List<DbColumn> FilterDbColumnList { get; private set; }
        public string PrimaryKey { get; private set; }
        public string ClassName { get; private set; }
        public string TableCommit { get; private set; }

        public DbHelper(BaseConfig config)
        {
            this.Config = config;
            DbColumnList = GetDbColumns(Config.ConnectionString, Config.DbDataBase, Config.TableName, null);
            FilterDbColumnList = Filter(DbColumnList, Config.NoExistFields);

            var tmp = DbColumnList.Where(p => p.IsPrimaryKey || p.ToString().ToLower().IndexOf("pkid") > -1);
            if (tmp == null || tmp.Count() <= 0)
            {
                PrimaryKey = DbColumnList.First().ColumnName;
            }
            else
            {
                PrimaryKey = DbColumnList.Where(p => p.IsPrimaryKey || p.ToString().ToLower().IndexOf("pkid") > -1).First().ColumnName;
            }
            ClassName = Config.TableName.Replace("_TBL", "").ToUpper();
            TableCommit = GetDbTables(Config.ConnectionString, Config.DbDataBase, Config.TableName)[0].Commit;
        }

        public List<DbTable> GetDbTables(string connectionstring, string database, string tables)
        {
            if (!string.IsNullOrEmpty(tables))
            {
                tables = string.Format(" and obj.name in ('{0}')", tables.Replace(",", "','"));
            }
            #region SQL

            string sql = string.Format(@"SELECT obj.name tablename,schem.name schemname,idx.rows,
                   cast
                  (case when (select count(0) from sys.indexes where object_id=obj.OBJECT_ID AND IS_PRIMARY_KEY=1) >=1 THEN 1 ELSE 0 END AS BIT)HasPrimaryKey,
                  isnull((select value from sys.extended_properties where obj.object_id = major_id and minor_id=0),'') as TableCommit
                  from {0}.sys.objects obj
				  inner join {0}.dbo.sysindexes idx on obj.object_id = idx.id and idx.indid<=1
				  inner join {0}.sys.schemas schem on obj.schema_id = schem.schema_id
				  where type='U' {1}
				  order by obj.name", database, tables);
            #endregion
            DataTable dt = GetDataTable(connectionstring, sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable
            {
                TableName = row.Field<string>("tablename"),
                SchemaName = row.Field<string>("schemname"),
                Rows = row.Field<int>("rows"),
                HasPrimaryKey = row.Field<bool>("HasPrimaryKey"),
                Commit = row.Field<string>("TableCommit")
            }).ToList();
        }

        public List<DbColumn> GetDbColumns(string connectionString, string database, string tablename, string schema)
        {
            if (string.IsNullOrWhiteSpace(schema)) schema = "dbo";
            #region SQL

            string sql = string.Format(@"
                    WITH indexCTE AS
                      (     SELECT ic.column_id,
                                   ic.index_column_id,
                                   ic.object_id
                            FROM Test.sys.indexes idx
                            INNER JOIN Test.sys.index_columns ic ON idx.index_id = ic.index_id
                            AND idx.object_id=ic.object_id
                            WHERE idx.object_id =OBJECT_ID(@tableName)
                            AND idx.is_primary_key=1
                       )
                    SELECT colm.column_id ColumnID,
                           cast(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS bit) IsPrimaryKey,
                           colm.name ColumnName,
                           systype.name ColumnType,
                           colm.is_identity IsIdentity,
                           colm.is_nullable IsNullable,
                           CAST(colm.max_length AS int) ByteLength,
                           ( CASE
                                 WHEN systype.name = 'nvarchar'
                                      AND colm.max_length>0 THEN colm.max_length/2
                                 WHEN systype.name = 'nchar'
                                      AND colm.max_length>0 THEN colm.max_length/2
                                 WHEN systype.name = 'ntext'
                                      AND colm.max_length>0 THEN colm.max_length/2
                                 ELSE colm.max_length
                             END ) AS CharLength,
                           CAST(colm.precision AS int)PRECISION, 
                           CAST(colm.scale AS int) SCALE, 
                           prop.value Remark
                    FROM Test.sys.columns colm
                    INNER JOIN Test.sys.types systype ON colm.system_type_id = systype.system_type_id
                            AND colm.user_type_id = systype.user_type_id
                    LEFT JOIN Test.sys.extended_properties prop ON colm.object_id = prop.major_id
                            AND colm.column_id = prop.minor_id
                    LEFT JOIN indexCTE ON colm.column_id = indexCTE.column_id
                            AND colm.object_id = indexCTE.object_id
                    WHERE colm.object_id = OBJECT_ID('Test.dbo.TestTable')
                         ORDER BY colm.column_id", database);
            #endregion
            SqlParameter param = new SqlParameter("@tableName", SqlDbType.NVarChar, 100) { Value = string.Format("{0}.{1}.{2}", database, schema, tablename) };

            DataTable dt = GetDataTable(connectionString, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new DbColumn()
            {
                ColumnID = row.Field<int>("ColumnID"),
                IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                IsIdentity = row.Field<bool>("IsIdentity"),
                ColumnName = row.Field<string>("ColumnName"),
                ColumnType = row.Field<string>("ColumnType"),

                IsNullable = row.Field<bool>("IsNullable"),
                ByteLength = row.Field<int>("ByteLength"),
                CharLength = row.Field<int>("CharLength"),
                Scale = row.Field<int>("Scale"),
                Remark = row["Remark"].ToString().Replace("\r\n", "")
            }).ToList();



        }

        public DataTable GetDataTable(string connectionstring, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                command.Parameters.AddRange(parms);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        public DbColumn GetCol(string columnname)
        {
            return DbColumnList.Where(p => p.ColumnName == columnname).First();
        }

        public List<DbColumn> Filter(List<DbColumn> list, string[] noExistFields)
        {
            return list.Where(p => noExistFields.Where(f => p.ColumnName.Contains(f)).Count() == 0).ToList();
        }
    }

    /// <summary>
    /// 表结构
    /// </summary>
    public sealed class DbTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表的架构
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 表的记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 是否含有主键
        /// </summary>
        public bool HasPrimaryKey { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>

        public string Commit { get; set; }
    }

    /// <summary>
    /// 表字段结构
    /// </summary>
    public sealed class DbColumn
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string CSharpType
        {
            get
            {
                return SqlServerDbTypeMap.MapCSharpType(ColumnType);
            }
        }

        /// <summary>
        /// 通用数据类型
        /// </summary>
        public Type CommonType
        {
            get
            {
                return SqlServerDbTypeMap.MapCommonType(ColumnType);
            }
        }

        /// <summary>
        /// 字节长度
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 字符长度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        /// 小数位
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 数据库&C#类型映射
    /// </summary>
    public class SqlServerDbTypeMap
    {
        public static string MapCSharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;

            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }

        public static Type MapCommonType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return Type.Missing.GetType();

            dbtype = dbtype.ToLower();
            Type commonType = typeof(object);
            switch (dbtype)
            {
                case "bigint": commonType = typeof(long); break;
                case "binary": commonType = typeof(byte[]); break;
                case "bit": commonType = typeof(bool); break;
                case "char": commonType = typeof(string); break;
                case "date": commonType = typeof(DateTime); break;
                case "datetime": commonType = typeof(DateTime); break;
                case "datetime2": commonType = typeof(DateTime); break;
                case "datetimeoffset": commonType = typeof(DateTimeOffset); break;
                case "decimal": commonType = typeof(decimal); break;
                case "float": commonType = typeof(double); break;
                case "image": commonType = typeof(byte[]); break;
                case "int": commonType = typeof(int); break;
                case "money": commonType = typeof(decimal); break;
                case "nchar": commonType = typeof(string); break;
                case "ntext": commonType = typeof(string); break;
                case "nvarchar": commonType = typeof(string); break;
                case "real": commonType = typeof(Single); break;
                case "smalldatetime": commonType = typeof(DateTime); break;
                case "smallmoney": commonType = typeof(decimal); break;
                case "sql_variant": commonType = typeof(object); break;
                case "sysname": commonType = typeof(object); break;
                case "text": commonType = typeof(string); break;
                case "time": commonType = typeof(TimeSpan); break;
                case "timestamp": commonType = typeof(byte[]); break;
                case "tinyint": commonType = typeof(byte); break;
                case "uniqueidentifier": commonType = typeof(Guid); break;
                case "varbinary": commonType = typeof(byte[]); break;
                case "varchar": commonType = typeof(string); break;
                case "xml": commonType = typeof(string); break;
                default: commonType = typeof(object); break;
            }
            return commonType;
        }
    }

}