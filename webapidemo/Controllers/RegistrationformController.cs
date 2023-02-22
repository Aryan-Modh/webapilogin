using LoginRegistrationApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationformController : ControllerBase
    {    
        
        //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        private readonly IConfiguration _configuration;

        public RegistrationformController(IConfiguration configuration)
        {
             _configuration = configuration;
        }

        [HttpPost]
        [Route("registrationform")]
        public string registrationform(Registrationform registrationform)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TestToy").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO registrationform(UserName,Password,Email,IsActive) " +
                "VALUES(' "+ registrationform.UserName.Trim() + "' , '"+ registrationform.Password.Trim()+ "' , '" 
                + registrationform.Email.Trim() + "' , '"+ registrationform.IsActive+"' )", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0) 
            {
                return "Data inserted";
            }

            else 
            {
                return "ERROR";
            }
            
            
        }

        [HttpPost]
        [Route("Login")]

        public string Login(Login registrationform)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TestToy").ToString()); 
            SqlDataAdapter da = new SqlDataAdapter
            ("SELECT * FROM Registrationform WHERE Email = '"+registrationform.Email+"' " +
            "AND Password = '"+registrationform.Password+"' AND IsActive = 1 ",con);
           
            DataTable dt = new DataTable();  
            da.Fill(dt);
            if(dt.Rows.Count > 0) 
            {
                return "Valid User";
            }

            else 
            {
                return "Invalid User";
            }
        } 
    }
}
