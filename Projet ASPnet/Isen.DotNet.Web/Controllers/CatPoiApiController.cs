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
    public class CatPoiApiController : Controller
    {
        private readonly ICatPoiRepository _catRepository;
        public CatPoiApiController(ICatPoiRepository icatrepo)
        {
            _catRepository = icatrepo;
        }
        
        //Toutes les categories
        [Route("api/categories/all")]
        public virtual JsonResult GetAllAction()
        {
            IEnumerable<CatPoi> allcats = _catRepository.GetAll();
            return Json(allcats);
        }
    }
}
