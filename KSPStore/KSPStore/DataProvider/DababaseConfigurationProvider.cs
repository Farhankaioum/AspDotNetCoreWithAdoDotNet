using KSPStore.Models;
using KSPStore.ViewModel.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace KSPStore.DataProvider
{

    public class DababaseConfigurationProvider
    {

        // For set database configuration
        // public string conString = "server=(localdb)\\MSSQLLocalDB;database=kspStoreDB_0001; Trusted_Connection=true";

        // IMemoryCache
        public static IMemoryCache cache;


        public static IConfiguration Configuration;

        // for ado.net
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd;
        public  ILogger<DababaseConfigurationProvider> logger;
         

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

        //For testing purpose Get All values from DB
        public IEnumerable<Employee> GetAllViaDataAdapter()
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                string query = "select * from Employee";
                SqlConnection co = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, co);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Emp");

                foreach (DataRow data in dataSet.Tables["Emp"].Rows)
                {
                    var model = new Employee
                    {
                        Id = (int)data["Id"],
                        Name = data["Name"].ToString(),
                        UserName = data["UserName"].ToString(),
                        Address = data["Address"].ToString(),
                        Email = data["Email"].ToString()
                    };
                    empList.Add(model);
                }

            }
            finally
            {
                con.Close();
            }
            return empList;
        }

        // For testing purpose get via id
        public Employee GetByIdTesting(int id)
        {
            string query = "select * from Employee where Id = " + id;
            SqlConnection co = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
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


        //For testing purpose SqlCommandBuilder
        public Employee LoadEmpById(int? id)
        {
            Employee model = null;
            if (id == null)
            {
                return new Employee();
            }
            string query = "select * from Employee where Id = " + id;
            SqlConnection co = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
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
        // for testing purpose
        public void UpdateEmpTesting(Employee model)
        {
            SqlConnection co = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cache.Get("query").ToString(), co);

            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = (DataSet)cache.Get("dataset");
            DataRow dr = ds.Tables["Emp"].Rows[0];
            dr["Name"] = model.Name;
            dr["UserName"] = model.UserName;
            dr["Address"] = model.Address;
            dr["Email"] = model.Email;

            dataAdapter.Update(ds, "Emp");

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



        // testing purpose: Get value from two different table
        public ReadTwoTableDataConcurrently GetAllValueFromDifferentTableViaDataAdapter()
        {
            ReadTwoTableDataConcurrently newDataFromTwoTable;

            string query = "select * from Employee; select * from EmpTags";
            SqlConnection co = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, co);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);
            dataSet.Tables[0].TableName = "Emp";
            dataSet.Tables[1].TableName = "Tags";

            List<Employee> employees = new List<Employee>();
            // read first table data
            foreach (DataRow em in dataSet.Tables["Emp"].Rows)
            {
                var emp = new Employee
                {
                    Id = (int)em["Id"],
                    Name = em["Name"].ToString(),
                    UserName = em["UserName"].ToString(),
                    Address = em["Address"].ToString(),
                    Email = em["Email"].ToString()
                };
                employees.Add(emp);
            }


            List<NewClass> secondData = new List<NewClass>();
            //read second table data
            foreach (DataRow em in dataSet.Tables["Tags"].Rows)
            {
                var ob = new NewClass
                {
                    Id = (int)em["Id"],
                    CategoryName = em["CategoryName"].ToString()
                };
                secondData.Add(ob);
            }

            newDataFromTwoTable = new ReadTwoTableDataConcurrently
            {
                Employees = employees,
                NewClasses = secondData
            };



            return newDataFromTwoTable;
        }




        // For Disconnected Model
        #region Disconnected Model
        public List<Employee> GetAllEmployeeUsingDisconnectedModel()
        {

            if (cache.Get("model") != null)
            {
                return GetAllEmpUsingDM();
            }
            string query = "select * from Employee";
            string conString1 = Configuration.GetConnectionString("KspStoreDBCon");
            SqlConnection con1 = new SqlConnection(conString1);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con1);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet, "Emp");
            dataSet.Tables["Emp"].PrimaryKey = new DataColumn[] { dataSet.Tables["Emp"].Columns["Id"] };// set primary key

            // caching DB value
            cache.Set("model", dataSet);

            List<Employee> employees = new List<Employee>();
            // read data from DataSet object
            foreach (DataRow em in dataSet.Tables["Emp"].Rows)
            {
                var emp = new Employee
                {
                    Id = (int)em["Id"],
                    Name = em["Name"].ToString(),
                    UserName = em["UserName"].ToString(),
                    Address = em["Address"].ToString(),
                    Email = em["Email"].ToString(),
                    Password = em["Password"].ToString()
                };
                employees.Add(emp);
            }
            return employees;
        }
        // For above method
        private List<Employee> GetAllEmpUsingDM()
        {
            DataSet dataSet = (DataSet)cache.Get("model");
            List<Employee> employees = new List<Employee>();
            // read data from DataSet object
            foreach (DataRow em in dataSet.Tables["Emp"].Rows)
            {
                if (em.RowState != DataRowState.Deleted)
                {
                    var emp = new Employee
                    {
                        Id = (int)em["Id"],
                        Name = em["Name"].ToString(),
                        UserName = em["UserName"].ToString(),
                        Address = em["Address"].ToString(),
                        Email = em["Email"].ToString(),
                        Password = em["Password"].ToString()
                    };
                    employees.Add(emp);
                }

            }
            return employees;

        }

        public void UpdateUsingDM(Employee model)
        {
            if (cache.Get("model") != null)
            {
                DataSet dataSet = (DataSet)cache.Get("model");

                DataRow dataRow = dataSet.Tables["Emp"].Rows.Find(model.Id);
                dataRow["Name"] = model.Name;
                dataRow["UserName"] = model.UserName;
                dataRow["Address"] = model.Address;
                dataRow["Email"] = model.Email;

                // Overwrite the dataset in cache
                cache.Set("model", dataSet);
            }


        }
        // get value via id
        public Employee FindByIdUsingDM(int id)
        {
            DataSet dataSet = (DataSet)cache.Get("model");

            DataRow dataRow = dataSet.Tables["Emp"].Rows.Find(id);
            var model = new Employee
            {
                Id = (int)dataRow["Id"],
                Name = dataRow["Name"].ToString(),
                UserName = dataRow["UserName"].ToString(),
                Address = dataRow["Address"].ToString(),
                Email = dataRow["Email"].ToString(),
                Password = dataRow["Password"].ToString()
            };
            return model;

        }
        public void DeleteUsingDM(int id)
        {

            if (cache.Get("model") != null)
            {
                DataSet dataSet = (DataSet)cache.Get("model");

                dataSet.Tables["Emp"].Rows.Find(id).Delete();// delete value

                // Overwrite the dataset in cache
                cache.Set("model", dataSet);

            }


        }

        // Final permanently update cache into database & clear cache
        public int UpdatePermanentIntoDBUsingDM()
        {
            int count = 0;
            if (cache.Get("model") != null)
            {

                string conString1 = Configuration.GetConnectionString("KspStoreDBCon");
                SqlConnection con1 = new SqlConnection(conString1);
                string query = "select * from Employee";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con1);

                DataSet ds = (DataSet)cache.Get("model");

                //insert command
                string strInsert = "insert into Employee values(@Name,@UserName,@Address,@Email,@Password)";
                SqlCommand insertCommand = new SqlCommand(strInsert, con1);
                insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
                insertCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");
                insertCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 60, "Address");
                insertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");
                insertCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                dataAdapter.InsertCommand = insertCommand;

                //update command
                string strUpdate = "Update Employee set Name=@Name, UserName=@UserName, Address=@Address," +
                    " Email=@Email, Password=@Password where Id=@Id";
                SqlCommand updateCommand = new SqlCommand(strUpdate, con1);
                updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
                updateCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50, "UserName");
                updateCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 60, "Address");
                updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");
                updateCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 50, "Password");
                updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                dataAdapter.UpdateCommand = updateCommand;

                // delete command
                string strDelete = "Delete from Employee where Id=@Id";
                SqlCommand deleteCommand = new SqlCommand(strDelete, con1);
                deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                dataAdapter.DeleteCommand = deleteCommand;

                count = dataAdapter.Update(ds, "Emp");

                //clear the cache


            }
            return count;

        }
        #endregion

        #region testing SqlBulkCopy
        public void CopyDataXmlToDBTable()
        {
            using SqlConnection con = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));

            DataSet ds = new DataSet();
            ds.ReadXml(new FileStream("data.xml", FileMode.Open));
            DataTable dtDept = ds.Tables["Department"];
            DataTable dtEmp = ds.Tables["Employee"];

            con.Open();
            using SqlBulkCopy bcdept = new SqlBulkCopy(con);
            bcdept.DestinationTableName = "tblDepartment";
            bcdept.ColumnMappings.Add("Id", "Id");
            bcdept.ColumnMappings.Add("Name", "Name");
            bcdept.ColumnMappings.Add("Location", "Location");
            bcdept.WriteToServer(dtDept);

            using SqlBulkCopy bcEmp = new SqlBulkCopy(con);
            bcEmp.DestinationTableName = "tblEmployee";
            bcEmp.ColumnMappings.Add("Id", "Id");
            bcEmp.ColumnMappings.Add("Name", "Name");
            bcEmp.ColumnMappings.Add("Gender", "Gender");
            bcEmp.ColumnMappings.Add("DepartmentId", "DepartmentId");
            bcEmp.WriteToServer(dtEmp);

        }

        public void CopyDateOneTableToAnother()
        {
            using SqlConnection sourceCon = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("select * from Products_Source", sourceCon);

            sourceCon.Open();
            using SqlDataReader rdr = cmd.ExecuteReader();

            using SqlConnection destinationCon = new SqlConnection(Configuration.GetConnectionString("KspStoreDBCon"));

            using SqlBulkCopy bc = new SqlBulkCopy(destinationCon);
            bc.BatchSize = 1000;
            bc.NotifyAfter = 5000;
            bc.SqlRowsCopied += (sender, e) =>
            {
                Console.WriteLine(e.RowsCopied + " loaded...");
            }; // using anonymous method
            bc.DestinationTableName = "Products_Destination";
            destinationCon.Open();
            bc.WriteToServer(rdr);

        }

     

        #endregion

    }



    public class Transfer<T> : IDisposable
    {
        private readonly string connectionString;

        protected readonly SqlConnection _sqlConnection;

        public Transfer(string connectionString)
        {
            this.connectionString = connectionString;
            _sqlConnection.Open();
        }

        // Transaction using
        public void TransferBalance(string fromAccount, string ToAccount)
        {
            var query = "Update Accounts set Balance = Balance  - 10 where AccountNumber = @AccountNumber";
            SqlTransaction transaction = _sqlConnection.BeginTransaction();
            var command = new SqlCommand(query, _sqlConnection, transaction);
            try
            {
                
                
                command.Parameters.AddWithValue("@AccountNumber", fromAccount);
                command.ExecuteNonQuery();

                query = "Update Accounts set Balance = Balance  + 10 where AccountNumber = @AccountNumber";
                command = new SqlCommand(query, _sqlConnection, transaction);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch 
            {
                transaction.Rollback();
            }
            
        }

        public void Dispose()
        {
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }
    }
    // testing purpose: Get value from two different table
    public class NewClass
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
