using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Isen.DotNet.Web.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Models.Base;
using Microsoft.Extensions.Logging;
using System.Dynamic;

namespace Isen.DotNet.Web.Controllers
{
    public class CommuneApiController : Controller
    {
        private readonly ICommuneRepository _comRepository;
        public CommuneApiController(ICommuneRepository icomrepo)
        {
            _comRepository = icomrepo;
        }
        
        //Toutes les communes
        [Route("api/communes/all")]
        public virtual JsonResult GetAllAction()
        {
            IEnumerable<Commune> allcoms = _comRepository.GetAll();
            return Json(allcoms);
        }
    }
}
