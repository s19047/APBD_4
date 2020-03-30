using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD_3.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        String CONNECTION_STRING = @"Data Source=LAPTOP-11FAC326\SQLEXPRESS;Initial Catalog=s19047;Integrated Security=True";
       [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            //check if studies exist or 404
            // Check if enrollment exists -> Insert
            //Check if index exists -> Insert/400
            //return Enrollment model
            //DTO - Data transfer Objects 


            using (var client = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand())
            {
                command.Connection = client;
                command.CommandText = "SELECT * FROM Studies where Name=@Name";
                command.Parameters.AddWithValue("Name", request.Studies);

                client.Open();
                var tran = client.BeginTransaction();

                var dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    //ERROR -404 studies does not exist
                }
                int idStudies = (int)dr["IdStudies"];
                command.CommandText = "SELECT * FROM Enrollment  where Semester=1 AND IdStudies=@IdStud";
                //manuel mapping 
                command.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName) VALUES (@FirstName,@LastName,...";
                command.Parameters.AddWithValue("FirstName", request.FirstName);

                //Inser Student if everything is ok
                tran.Commit();


            }
                     

                  
                return Ok();
        }
   
        public IActionResult PromoteStudents()
        {
            //check if studies exists
            //Find all the students froms tudies = IT and semester=1
            //Promote all student to the 2 semester
            // find an enrollment record with studies= IT and semester = 2 -> IdEnrollment=10


            //create stored procedure 
            return Ok();
        }
    }
}