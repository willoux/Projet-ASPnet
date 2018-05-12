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
    public class PoiApiController : Controller
    {
        private readonly IPoiRepository _poiRepository;
        public PoiApiController(IPoiRepository ipoirepo)
        {
            _poiRepository = ipoirepo;
        }
        
        //Tous les Pois
        [Route("api/pois/all")]
        public virtual JsonResult GetAllAction()
        {
            IEnumerable<Poi> allpois = _poiRepository.GetAll();
            return Json(allpois);
        }
        
        //Tous les Pois avec le meme departement passé en param
        [Route("api/pois/departement/{Dep}")]
        public virtual JsonResult GetDepartementAction(string Dep)
        {
            IEnumerable<Poi> allpois = _poiRepository.GetAll();
            List<Poi> deppois = new List<Poi>();
            foreach (var poi in allpois)
            {
                if (poi.Address.Commune.Departement.Name == Dep)
                {
                    deppois.Add(poi);
                }
            }
            return Json(deppois);
        }
        
        //Tous les Pois avec la meme commune passée en param
        [Route("api/pois/commune/{Com}")]
        public virtual JsonResult GetCommuneAction(string Com)
        {
            IEnumerable<Poi> allpois = _poiRepository.GetAll();
            List<Poi> compois = new List<Poi>();
            foreach (var poi in allpois)
            {
                if (poi.Address.Commune.Name == Com)
                {
                    compois.Add(poi);
                }
            }
            return Json(compois);
        }
        
        //Tous les Pois avec la meme catégorie passée en param
        [Route("api/pois/categorie/{Cat}")]
        public virtual JsonResult GetCategorieAction(string Cat)
        {
            IEnumerable<Poi> allpois = _poiRepository.GetAll();
            List<Poi> catpois = new List<Poi>();
            foreach (var poi in allpois)
            {
                if (poi.Category.Name == Cat)
                {
                    catpois.Add(poi);
                }
            }
            return Json(catpois);
        }
        
        
    }
}
