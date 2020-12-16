using LogitWebApp.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.Admin_RoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
    }
}
