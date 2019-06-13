using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AgendaDbContext db;

        public ValuesController(AgendaDbContext db)
        {
            this.db = db;
        }

        //AgendaDbContext db;

        // GET api/values
        [HttpGet]
        public ActionResult<Agenda[]> Get()
        {
            var contato = db.Agenda.ToArray();
            if (contato != null)
            {
                return contato;
            }
            else
            {
                return null;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Agenda> Get(int id)
        {
            Agenda contato = db.Agenda.Where(a => a.ID == id).FirstOrDefault();
            if (contato != null)
            {
                return contato;
            }
            else
            {
                return null;
            }
        }

        // POST api/values
        [HttpPost]
        public void Post(Agenda agenda)
        {
            try
            {
                if (agenda.ID == 0)
                {
                    db.Agenda.Add(agenda);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(agenda).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("PRIMARY"))
                {
                    db.Entry(agenda).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var contato = db.Agenda.Find(id);
            db.Agenda.Remove(contato);
            db.SaveChanges();
        }
    }
}
