using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FindTheClueAPI;

namespace FindTheClueAPI.Controllers
{
    public class TeamsController : ApiController
    {
        private findthecluedbEntities db = new findthecluedbEntities();

        public TeamsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Teams
        public IQueryable<team> Getteams()
        {
            return db.teams;
        }

        // GET: api/Teams/5
        [ResponseType(typeof(team))]
        public IHttpActionResult Getteam(int id)
        {
            team team = db.teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putteam(int id, team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.id_team)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teams
        [ResponseType(typeof(team))]
        public IHttpActionResult Postteam(team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.id_team }, team);
        }

        // DELETE: api/Teams/5
        [ResponseType(typeof(team))]
        public IHttpActionResult Deleteteam(int id)
        {
            team team = db.teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teamExists(int id)
        {
            return db.teams.Count(e => e.id_team == id) > 0;
        }
    }
}