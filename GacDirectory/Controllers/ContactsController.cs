using GacDirectory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GacDirectory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]    
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private DataContext dataContext;

        public ContactsController(ILogger<ContactsController> logger, DataContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        [HttpGet(Name = "GetEmployeeData")]
        public object? GetEmployeeData(int? CompanyID = null)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiEmployeeData] @CompanyID";
            sqlParams[0] = new SqlParameter { ParameterName = "@CompanyID", Value = CompanyID is null ? DBNull.Value : CompanyID, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.EmployeeData.FromSqlRaw(sqlQuery, sqlParams);

            if (result is not null)
                return result.ToList();
            else
                return null;
        }

        //[AllowAnonymous]
        [HttpGet(Name = "GetContactData")]
        public object? GetContactData(string Code)
        {
            if (Code == null)
                return "Code value is not provided.";

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiEmployeeContact] @Code";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.EmployeeContact.FromSqlRaw(sqlQuery, sqlParams);

            if (result is not null)
                return result.ToList();
            else
                return null;
        }

        [HttpGet(Name = "GetFavorites")]
        public object? GetFavorites()
        {
            var user = User;

            if (!user.HasClaim(c => c.Type == "Code"))
            {
                return Unauthorized();
            }

            string Code = user.Claims.Where(c => c.Type == "Code").FirstOrDefault().Value;

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "Select EmployeeID, AttenCode, ContactFav From Employee Where AttenCode=@Code"; ;
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.Employee.FromSqlRaw(sqlQuery, sqlParams);

            if (result is not null)
                return result.FirstOrDefault().ContactFav;
            else
                return null;
        }

        
        [HttpPost(Name = "SaveFavorites")]
        public object SaveFavorites([FromBody] EmployeeFav employeeFav)
        {
            string Favs="", Code = "";

            if (employeeFav.Code == null)
            {
                var user = User;

                if (!user.HasClaim(c => c.Type == "Code"))
                {
                    return Unauthorized();
                }

                Code = user.Claims.FirstOrDefault(c => c.Type == "Code").Value;
            }
            else
            {
                Code = employeeFav.Code;
            }

            Favs = employeeFav.Favs;

            var commandText = "Update Employee Set ContactFav=@Favs Where AttenCode=@Code";

            SqlParameter[] sqlParams = new SqlParameter[2];            
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };
            sqlParams[1] = new SqlParameter { ParameterName = "@Favs", Value = Favs, Direction = System.Data.ParameterDirection.Input };

            using (var context = dataContext)
            {
                var result = dataContext.Database.ExecuteSqlRaw(commandText, sqlParams);

                if (result > 0)
                    return true;
                else
                    return false;
            }            
        }


        [HttpGet(Name = "GetFavContacts")]
        public object? GetFavContacts()
        {
            var user = User;

            if (!user.HasClaim(c => c.Type == "Code"))
            {
                return Unauthorized();
            }

            string Code = user.Claims.Where(c => c.Type == "Code").FirstOrDefault().Value;

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiGetFavContacts] @Code";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.EmployeeContact.FromSqlRaw(sqlQuery, sqlParams);

            if (result is not null)
                return result.ToList();
            else
                return null;
        }


        [HttpGet(Name = "SearchContacts")]
        public object? SearchContacts(string name)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            string sqlQuery = "[ApiSearchContacts] @Company, @Code, @Branch, @Department, @Division, @EmployeeID, @Name, @Starting";
            sqlParams[0] = new SqlParameter { ParameterName = "@Company", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[1] = new SqlParameter { ParameterName = "@Code", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[2] = new SqlParameter { ParameterName = "@Branch", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[3] = new SqlParameter { ParameterName = "@Department", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[4] = new SqlParameter { ParameterName = "@Division", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[5] = new SqlParameter { ParameterName = "@EmployeeID", Value = DBNull.Value, Direction = System.Data.ParameterDirection.Input };
            sqlParams[6] = new SqlParameter { ParameterName = "@Name", Value = name, Direction = System.Data.ParameterDirection.Input };
            sqlParams[7] = new SqlParameter { ParameterName = "@Starting", Value = 0, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.EmployeeContact.FromSqlRaw(sqlQuery, sqlParams);

            if (result is not null)
                return result.ToList();
            else
                return null;
        }
    }
}