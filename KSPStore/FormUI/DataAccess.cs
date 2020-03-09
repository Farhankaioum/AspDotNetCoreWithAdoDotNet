using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FormUI
{
    public class DataAccess
    {
        public List<Person> GetPeople(string lastName)
        {
            using IDbConnection connection = new SqlConnection(Helper.CnnVal(""));

            var output = connection.Query<Person>("dbo.People_GetByLastName @LastName", new { LastName = lastName }).ToList();// for SP 

            return connection.Query<Person>("select * from People where LastName = '" + lastName + "'").ToList();
           
            
        }
    }
}
