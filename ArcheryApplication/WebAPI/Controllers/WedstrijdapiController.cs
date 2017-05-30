using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using ArcheryApplication.Classes;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class WedstrijdapiController : ApiController
    {
        private WedstrijdRepository wedstrijdrepo = new WedstrijdRepository(new MysqlWedstrijdLogic());
        // GET api/<controller>
        public List<Wedstrijd> Get()
        {
            var wedstrijden = wedstrijdrepo.GetAllWedstrijden();
            return wedstrijden;
        }

        // GET api/<controller>/5
        public Wedstrijd Get(int id)
        {
            return wedstrijdrepo.GetWedstrijdById(id);
        }
    }
}