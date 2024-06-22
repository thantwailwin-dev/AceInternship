using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace InternshipDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                /*foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }*/
                var paraArr = parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray();
                command.Parameters.AddRange(paraArr);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            string json = JsonConvert.SerializeObject(dt); // C# -> Json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //Json -> C#

            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                /*foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }*/
                var paraArr = parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray();
                command.Parameters.AddRange(paraArr);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            string json = JsonConvert.SerializeObject(dt); // C# -> Json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //Json -> C#

            return lst[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {               
                var paraArr = parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray();
                command.Parameters.AddRange(paraArr);
            }

            var result = command.ExecuteNonQuery();

            connection.Close();            

            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter()
        {

        }

        public AdoDotNetParameter(string name,object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
