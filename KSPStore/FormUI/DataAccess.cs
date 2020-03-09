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

        public void InsertPerson(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            using IDbConnection connection = new SqlConnection(Helper.CnnVal(""));

            List<Person> people = new List<Person>();
            people.Add(new Person { FirstName = firstName, LastName = lastName, EmailAddress = emailAddress, PhoneNumber = phoneNumber});

            connection.Execute("dbo.Peopel_Indesr @FirstName, @LastName, @EmailAddress, @PhoneNumber", people);
        }
        
    }
}
