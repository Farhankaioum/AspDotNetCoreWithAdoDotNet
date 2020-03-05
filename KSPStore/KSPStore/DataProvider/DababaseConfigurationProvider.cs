using KSPStore.Models;
using KSPStore.ViewModel.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Providers.Entities;

namespace KSPStore.DataProvider
{

    public class DababaseConfigurationProvider
    {
        // For set database configuration
        public string conString = "server=(localdb)\\MSSQLLocalDB;database=kspStoreDB_0001; Trusted_Connection=true";

        // IMemoryCache
        public static IMemoryCache cache;


        public static IConfiguration Configuration;

        // for ado.net
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd;

        public DababaseConfigurationProvider()
        {
            // for get configuration connectionString from appsettings.json
            con.ConnectionString = Configuration.GetConnectionString("KspStoreDBCon");
            cmd = con.CreateCommand();
        }
        // insert method
        public void Insert(Employee model)
        {
            try
            {
                string query = "Insert into Employee values('" + model.Name + "', '" + model.UserName + "', '" + model.Address + "'," +
                    " '" + model.Email + "', '" + model.Password + "')";

                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteScalar();
            }
            finally
            {
                con.Close();
            }
        }

        // update method
        public void Update(Employee model)
        {
            try
            {
                string query = "Update Employee set " +
                    "Name = '" + model.Name + "', UserName = '" + model.UserName + "',  Address = '" + model.Address + "', Email = '" + model.Email + "'," +
                    " Password='" + model.Password + "' where Id = " + model.Id;


                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteScalar();
            }
            finally
            {
                con.Close();
            }
        }
        // Get all employee method
        public IEnumerable<Employee> GetAll()
        {
            List<Employee> empList = new List<Employee>();
            try
            {

                string query = "select * from Employee";
                cmd.CommandText = query;

                con.Open();
                var data = cmd.ExecuteReader();
                while (data.Read())
                {
                    var emp = new Employee
                    {
                        Id = (int)data["Id"],
                        Name = data["Name"].ToString(),
                        UserName = data["UserName"].ToString(),
                        Address = data["Address"].ToString(),
                        Email = data["Email"].ToString(),
                        Password = data["Password"].ToString()
                    };

                    empList.Add(emp);

                }

            }
            finally
            {
                con.Close();
            }
            return empList;
        }

        // get via id
        public Employee GetById(int id)
        {
            string query = "select * from Employee where Id = " + id;

            cmd.CommandText = query;
            var model = new Employee();

            try
            {
                con.Open();
                var data = cmd.ExecuteReader();


                while (data.Read())
                {

                    model.Id = (int)data["Id"];
                    model.Name = data["Name"].ToString();
                    model.UserName = data["UserName"].ToString();
                    model.Address = data["Address"].ToString();
                    model.Email = data["Email"].ToString();
                    model.Password = data["Password"].ToString();

                }

            }
            finally
            {
                con.Close();
            }

            return model;
        }

        // For testing purpose get via id
        public Employee GetByIdTesting(int id)
        {
            string query = "select * from Employee where Id = " + id;
            SqlConnection co = new SqlConnection(conString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, co);
            
            DataSet dataSet = new DataSet();
            var model = new Employee();

            try
            {
                
                dataAdapter.Fill(dataSet, "Emp");
                foreach (DataRow row in dataSet.Tables["Emp"].Rows)
                {
                    model.Id = Convert.ToInt32(row["Id"]);
                    model.Name = row["Name"].ToString();
                    model.UserName = row["UserName"].ToString();
                    model.Email = row["Email"].ToString();
                    model.Address = row["Address"].ToString();
                }

                
            }
            finally
            {
                con.Close();
            }

            return model;
        }

        // For testing purpose IMemoryCache
        public void Test()
        {
            SqlCommandBuilder builder = new SqlCommandBuilder();
          
        }
        //For testing purpose SqlCommandBuilder
        public Employee LoadEmpById(int? id)
        {
            Employee model = null;
            if (id == null)
            {
                return new Employee();
            }
            string query = "select * from Employee where Id = " + id;
            SqlConnection co = new SqlConnection(conString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, co);
            DataSet ds = new DataSet();

            dataAdapter.Fill(ds, "Emp");

            cache.Set("query", query);
            cache.Set("dataset", ds);

            if (ds.Tables["Emp"].Rows.Count > 0)
            {
                DataRow dataRow = ds.Tables["Emp"].Rows[0];
                 model = new Employee
                {
                    Id = (int)dataRow["Id"],
                    Name = dataRow["Name"].ToString(),
                    UserName = dataRow["UserName"].ToString(),
                    Address = dataRow["Address"].ToString(),
                    Email = dataRow["Email"].ToString()
                };
               
            }
            return model;
           
        }
        public void UpdateEmpTesting(Employee model)
        {
            SqlConnection co = new SqlConnection(conString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cache.Get("query").ToString(), co);

            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = (DataSet)cache.Get("dataset");
            DataRow dr = ds.Tables["Emp"].Rows[0];
            dr["Name"] = model.Name;
            dr["UserName"] = model.UserName;
            dr["Address"] = model.Address;
            dr["Email"] = model.Email;

            int rowsUpdated = dataAdapter.Update(ds, "Emp");
            
        } 

        // Delete method via id
        public void Delete(int id)
        {
            string query = "delete Employee where Id = " + id;
            cmd.CommandText = query;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }

        }

        //testing purpose
        public ReadTwoTableDataConcurrently GetAllValueFromDifferentTable()
        {
            ReadTwoTableDataConcurrently newDataFromTwoTable;
            try
            {
                string query = "select * from Employee; select * from EmpTags";
                cmd.CommandText = query;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                // read first table data
                while (reader.Read())
                {
                    var emp = new Employee
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    employees.Add(emp);



                }

                // testing purpose: Get value from two different table
                List<NewClass> secondData = new List<NewClass>();
                reader.NextResult();
                while (reader.Read())
                {
                    var data = new NewClass
                    {
                        Id = (int)reader["Id"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    secondData.Add(data);
                }

                newDataFromTwoTable = new ReadTwoTableDataConcurrently
                {
                    Employees = employees,
                    NewClasses = secondData
                };
                reader.Close();
            }
            finally
            {

                con.Close();
            }
            return newDataFromTwoTable;
        }
    }
    // testing purpose: Get value from two different table
    public class NewClass
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
