using Library.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/update")]
    public class UpdatesController : Controller
    {
        public UpdatesController()
        {

        }

        [HttpGet]
        public IActionResult CheckForUpdate()
        {
            return Json(new UpdateDto()
            {
                IsUpdate = true,
                Url = "https://libraryapi.azurewebsites.com/",
                UpdateMessage = "\nAttention!\nFor versions of this app distributed for judging purposes:\n\n\tThere has been an update to passowrds for security purposes. \n\tThe student user password is now: FBLAStudent!2018 \n\tIn addition, the librarian user password is now: FBLALibrarian!2018"
            });
        }
    }
}
