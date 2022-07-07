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
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private DataContext dataContext;

        public EmployeeController(ILogger<EmployeeController> logger, DataContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        [HttpGet(Name = "GetLoggedUserData")]
        public object? GetLoggedUserData()
        {
            var user = User;

            if (!user.HasClaim(c => c.Type == "Code"))
            {
                var errorRes = new EmployeeData();
                errorRes.Id = -1;
                errorRes.Name = "Invalid request. Not Authorized!";
                return errorRes;
            }

            string pCode = user.Claims.Where(c => c.Type == "Code").FirstOrDefault().Value;

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiEmployeeContact] @Code";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = pCode, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.EmployeeContact.FromSqlRaw(sqlQuery, sqlParams).ToList();

            if (result != null)
                return result;
            else
                return null;
        }

        [HttpGet(Name = "GetLeaveBalance/{Code:string?}")]
        public object? GetLeaveBalance(string? Code = null)
        {
            if (Code == null)
            {
                var user = User;

                if (!user.HasClaim(c => c.Type == "Code"))
                {
                    var errorRes = new EmployeeData();
                    errorRes.Id = -1;
                    errorRes.Name = "Invalid request. Not Authorized!";
                    return errorRes;
                }

                Code = user.Claims.FirstOrDefault(c => c.Type == "Code").Value;
            }

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiLeaveBalance] @Code";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.LeaveBalance.FromSqlRaw(sqlQuery, sqlParams).ToList();
            //var result = res.ToList();

            //if (result is not null)
            //    return result.ToList();
            //else
            //    return null;

            if (result is not null)
                return result;
            else
                return null;
        }

        //[AllowAnonymous]
        [HttpGet(Name = "GetLeaveReport/{Code:string?}/{Year=int?}")]
        public object? GetLeaveReport(string? Code = null, int? Year = null)
        {
            if (Code == null)
            {
                var user = User;

                if (!user.HasClaim(c => c.Type == "Code"))
                {
                    var errorRes = new EmployeeData();
                    errorRes.Id = -1;
                    errorRes.Name = "Invalid request. Not Authorized!";
                    return errorRes;
                }

                Code = user.Claims.FirstOrDefault(c => c.Type == "Code").Value;
            }

            if (Year is null)
                Year = DateTime.Now.Year;

            SqlParameter[] sqlParams = new SqlParameter[2];
            string sqlQuery = "[ApiLeaveReport] @Code, @Year";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };
            sqlParams[1] = new SqlParameter { ParameterName = "@Year", Value = Year, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.LeaveReport.FromSqlRaw(sqlQuery, sqlParams).ToList();
            
            if (result is not null)
                return result;
            else
                return null;
        }

        [HttpGet(Name = "GetDepartmentChart/{Code:string?}")]
        public object? GetDepartmentChart(string? Code = null)
        {
            if (Code == null)
            {
                var user = User;

                if (!user.HasClaim(c => c.Type == "Code"))
                {
                    var errorRes = new DepartmentChart();
                    errorRes.Id = -1;
                    errorRes.Name = "Invalid request. Not Authorized!";
                    return errorRes;
                }

                Code = user.Claims.FirstOrDefault(c => c.Type == "Code").Value;
            }

            SqlParameter[] sqlParams = new SqlParameter[1];
            string sqlQuery = "[ApiDepartmentChart] @Code";
            sqlParams[0] = new SqlParameter { ParameterName = "@Code", Value = Code, Direction = System.Data.ParameterDirection.Input };

            var result = dataContext.DepartmentChart.FromSqlRaw(sqlQuery, sqlParams).ToList();

            if (result != null)
                return result;
            else
                return null;
        }
    }
}