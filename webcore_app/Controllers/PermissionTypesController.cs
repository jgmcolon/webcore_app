using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webcore_app.Core.Database;
using webcore_app.Core.Interfaces;
using webcore_app.Response;
using webnet_app.domain.Entities.Permission;
using AppContext = webcore_app.Core.Database.AppContext;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace webcore_app.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionTypesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private IUnitOfWork<Core.Database.AppContext> _unitOfWork;

        public PermissionTypesController(
            IUnitOfWork<Core.Database.AppContext> unitOfWork,
            ILogger<PermissionTypesController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = (ILogger)logger;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }



        [HttpGet("")]
        public ListResponse GetList()
        {
            return new ListResponse
            {
                List = _unitOfWork.Context.PermissionTypes
                                  
                                  .OrderBy(x => x.Id)
                                  .Select(x => new 
                                  {
                                      Id = x.Id,
                                      Description = x.Description
                                  }).ToList()
            };
        }

    }
}
