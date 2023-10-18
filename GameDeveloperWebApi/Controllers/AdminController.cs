using GameDeveloperBusiness.Abstract;
using GameDeveloperEntity.Dto.Admin.Login;
using GameDeveloperEntity.Dto.Admin.Register;
using GameDeveloperEntity.Dto.Admin.Update;
using Microsoft.AspNetCore.Mvc;

namespace GameDeveloperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("register")]
        public object Register(AdminRegisterRequestDto adminRegisterRequestDto)
        {
            return _adminService.Register(adminRegisterRequestDto);
        }

        [HttpPost]
        [Route("login")]
        public object Login(AdminLoginRequestDto adminLoginRequestDto)
        {
            return _adminService.Login(adminLoginRequestDto);
        }

        [HttpPost]
        [Route("update")]
        public object Update(AdminUpdateRequestDto adminUpdateRequestDto)
        {
            return _adminService.UpdateAdmin(adminUpdateRequestDto);
        }

        [HttpGet]
        [Route("getAdmins")]
        public object GetAdmins()
        {
            return _adminService.GetAdmins();
        }

        [HttpGet]
        [Route("getUsers")]
        public object GetUsers()
        {
            return _adminService.GetUsers();
        }
    }
}
