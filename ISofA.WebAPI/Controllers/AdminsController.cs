using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Filters;
using ISofA.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    [ISofAAuthorization(Role = ISofAUserRole.SysAdmin)]
    [RoutePrefix("api/Theaters/{theaterId}/Admins")]
    public class AdminsController : ApiController
    {
        private readonly IAdminService _adminService;

        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("~/api/SysAdmin")]
        public async Task<IHttpActionResult> PostSysAdminAsync(AdminBindingModel theaterAdmin)
        {
            ISofAUser user = new ISofAUser
            {
                UserName = theaterAdmin.Email,
                Name = theaterAdmin.Name,
                Surname = theaterAdmin.Surname,
                Email = theaterAdmin.Email,
                City = theaterAdmin.City,
                PhoneNumber = theaterAdmin.PhoneNumber,
                ISofAUserRole = ISofAUserRole.SysAdmin
            };

            IdentityResult result = await UserManager.CreateAsync(user, "Admin123!");

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            } else
            {
                return Ok();
            }
        }

        [Route("")]
        public IEnumerable<ISofAUserDTO> Get(int theaterId, [FromUri] String role = "")
        {
            if (string.IsNullOrEmpty(role))
                return _adminService.GetAdmins(theaterId);
            else if (role == "theater")
                return _adminService.GetTheaterAdmins(theaterId);
            else if (role == "fanzone")
                return _adminService.GetFanZoneAdmins(theaterId);
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("")]
        public async Task<IHttpActionResult> PostAsync(int theaterId, AdminBindingModel theaterAdmin)
        {
            ISofAUser user = new ISofAUser
            {
                UserName = theaterAdmin.Email,
                Name = theaterAdmin.Name,
                Surname = theaterAdmin.Surname,
                Email = theaterAdmin.Email,
                City = theaterAdmin.City,
                PhoneNumber = theaterAdmin.PhoneNumber,
            };

            IdentityResult result = await UserManager.CreateAsync(user, "Password1!");

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            ISofAUserDTO admin = null;

            if (theaterAdmin.AdminType == 0)
            {
                admin = _adminService.AddTheaterAdmin(theaterId, user);
            }
            else if (theaterAdmin.AdminType == 1)
            {
                admin = _adminService.AddFanZoneAdmin(theaterId, user);
            }

            if (admin != null)
            {
                return Ok(admin);
            }

            return BadRequest();
        }

        [Route("{theaterAdminId}")]
        public void Delete(int theaterId, string theaterAdminId)
        {
            _adminService.RemoveAdmin(theaterId, theaterAdminId);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
