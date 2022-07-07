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
    public class PortalController : ControllerBase
    {
        private readonly ILogger<PortalController> _logger;
        private DataContext dataContext;

        public PortalController(ILogger<PortalController> logger, DataContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        [HttpGet(Name = "GetPublicHolidays")]
        public object? GetPublicHolidays()
        {                        
            string sqlQuery = "[ApiPublicHolidays]";            

            var result = dataContext.PublicHolidays.FromSqlRaw(sqlQuery);            

            if (result is not null)
                return result.ToList();
            else
                return null;
        }
    }
}