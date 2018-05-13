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
    public class DepartementApiController : Controller
    {
        private readonly IDepartementRepository _depRepository;
        public DepartementApiController(IDepartementRepository ideprepo)
        {
            _depRepository = ideprepo;
        }
        
        //Tous les Departements
        [Route("api/departements/all")]
        public virtual JsonResult GetAllAction()
        {
            IEnumerable<Departement> alldeps = _depRepository.GetAll();
            return Json(alldeps);
        }
    }
}
