using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace LiberacaoCredito.Devedor.Infra.Common.DapperExtensions
{
    [ExcludeFromCodeCoverage]
    public static class SqlExtensionFunction
    {
        public static string SelectQuery<T>(string alias = null)
        {
            List<string> fields = new List<string>();
            List<string> values = new List<string>();

            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is QueryFieldAttribute queryFieldAttr)
                    {
                        object propName = prop.Name;
                        string auth = queryFieldAttr.Nome;

                        string authQuery = !string.IsNullOrEmpty(alias) ? $@"{alias}.{auth}" : auth;
                        fields.Add(authQuery + " AS " + propName);

                        if (prop.PropertyType.FullName == typeof(DateTime).FullName)
                        {
                            values.Add($"@CONVERT(CHAR(10), {propName}, 103");
                        }
                        else
                        {
                            values.Add(propName?.ToString());
                        }
                    }
                }
            }

            QueryTableAttribute tableName = typeof(T).GetCustomAttribute(typeof(QueryTableAttribute)) as QueryTableAttribute;

            return $"SELECT { fields.Aggregate((i, j) => i + "," + j) } FROM { tableName.Nome }";
        }
        public static string SelectQueryFirst<T>(string alias = null)
        {
            List<string> fields = new List<string>();
            List<string> values = new List<string>();

            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is QueryFieldAttribute queryFieldAttr)
                    {
                        object propName = prop.Name;
                        string auth = queryFieldAttr.Nome;

                        string authQuery = !string.IsNullOrEmpty(alias) ? $@"{alias}.{auth}" : auth;
                        fields.Add(authQuery + " AS " + propName);

                        if (prop.PropertyType.FullName == typeof(DateTime).FullName)
                        {
                            values.Add($"@CONVERT(CHAR(10), {propName}, 103");
                        }
                        else
                        {
                            values.Add(propName?.ToString());
                        }
                    }
                }
            }

            QueryTableAttribute tableName = typeof(T).GetCustomAttribute(typeof(QueryTableAttribute)) as QueryTableAttribute;

            return $"SELECT Top(1) { fields.Aggregate((i, j) => i + "," + j) } FROM { tableName.Nome }";
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class QueryFieldAttribute : Attribute
    {
        // Private fields.
        private readonly string nome;

        public QueryFieldAttribute(string name)
        {
            nome = name;
        }

        // Define Name property.
        // This is a read-only attribute.
        public virtual string Nome => nome;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class QueryTableAttribute : Attribute
    {
        // Private fields.
        private readonly string nome;

        public QueryTableAttribute(string name)
        {
            nome = name;
        }

        // Define Name property.
        // This is a read-only attribute.
        public virtual string Nome => nome;
    }
}
