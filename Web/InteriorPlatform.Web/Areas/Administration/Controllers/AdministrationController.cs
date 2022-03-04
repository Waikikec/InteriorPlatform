﻿namespace InteriorPlatform.Web.Areas.Administration.Controllers
{
    using InteriorPlatform.Common;
    using InteriorPlatform.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
