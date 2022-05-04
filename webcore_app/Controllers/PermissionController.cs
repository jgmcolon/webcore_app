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
using webcore_app.Core.Extensions;
using webcore_app.Core.Interfaces;
using webcore_app.Request;
using webcore_app.Response;
using webnet_app.domain.Entities.Permission;
using webnet_app.domain.ViewModel;
using AppContext = webcore_app.Core.Database.AppContext;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace webcore_app.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private IUnitOfWork<Core.Database.AppContext> _unitOfWork;

        public PermissionController(
            IUnitOfWork<Core.Database.AppContext> unitOfWork,
            ILogger<PermissionController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = (ILogger)logger;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("")]
        public TableResponse GetAll([FromHeader(Name = "page")] int Page = 0,
                                            [FromHeader(Name = "limit")] int perPage = 15,
                                            [FromHeader(Name = "search")] string searchText = "")
        {
            int skip = Page * perPage;

            return new TableResponse
            {
                page = Page,
                perPage = perPage,
                total = _unitOfWork.Context.Permissions.Count(),
                data = _unitOfWork.Context.Permissions.Skip(skip).Take(perPage)
                                  .Include(e => e.PermissionType)
                                    .Select(x => new PermissionViewModel
                                    {
                                        RowId = x.RowId.ToString(),
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        PermissionType = x.PermissionType.Description,
                                        Id = x.Id,
                                        PermissionDate = x.PermissionDate.ToString("dd/MMM/yyyy")
                                    }).ToList()
            };
        }





        [HttpGet("{id}")]
        public ItemResponse GetById(string id)
        {
            var row = _unitOfWork.Context.Permissions
                                  .Include(e => e.PermissionType)
                .Where(x => x.RowId.Equals(id.ToGuid())).FirstOrDefault();

            if (row is null) return new ItemResponse { Error = true, Message = "Registro no encontrado" };

            return new ItemResponse
            {
                Row = new {
                    RowId = row.RowId.ToString(),
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    PermissionType = row.PermissionType.Description,
                    PermissionTypeId = row.PermissionTypeId,
                    Id = row.Id,
                    PermissionDate = row.PermissionDate
                }

            };
        }

        [HttpPost()]
        public BaseResponse Insert([FromBody] PermissionRequest value)
        {


            _unitOfWork.Context.Permissions.Add(new Permission
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                PermissionTypeId = value.PermissionTypeId,
                PermissionDate = value.PermissionDate
            });
            _unitOfWork.Context.SaveChanges();

            return new BaseResponse { Message = "Registro insertado con exito." };
        }

        [HttpPost("{id}")]
        public BaseResponse Update(string id, [FromBody] PermissionRequest value)
        {
            var row = _unitOfWork.Context.Permissions.Where(x => x.RowId.Equals(id.ToGuid())).FirstOrDefault();
            if (row is null) return new BaseResponse { Error = true, Message = "Registro no encontrado" };



            row.FirstName = value.FirstName;
            row.LastName = value.LastName;
            row.PermissionTypeId = value.PermissionTypeId;
            row.PermissionDate = value.PermissionDate;

            _unitOfWork.Context.Permissions.Update(row);
            _unitOfWork.Context.SaveChanges();

            return new BaseResponse { Message = "Registro actualizado con exito." };
        }

        [HttpPost("{id}")]
        public BaseResponse Delete(string id)
        {
            var row = _unitOfWork.Context.Permissions.Where(x => x.RowId.Equals(id.ToGuid())).FirstOrDefault();
            if (row is null) return new BaseResponse { Error = true, Message = "Registro no encontrado" };

            _unitOfWork.Context.Permissions.Remove(row);

            _unitOfWork.Context.SaveChanges();

            return new BaseResponse { Message = "Registro actualizado con exito." };
        }

        [HttpGet("")]
        public ListResponse GetList()
        {
            return new ListResponse
            {
                List = _unitOfWork.Context
                                  .Permissions
                                  .Include(e => e.PermissionType)
                                  .OrderBy(x => x.Id)
                                  .Select(x => new PermissionViewModel
                                  {
                                      RowId = x.RowId.ToString(),
                                      FirstName = x.FirstName,
                                      LastName = x.LastName,
                                      PermissionType = x.PermissionType.Description,
                                      Id = x.Id,
                                      PermissionDate = x.PermissionDate.ToString("dd/MMM/yyyy")
                                  }).ToList()
            };
        }
    }
}
