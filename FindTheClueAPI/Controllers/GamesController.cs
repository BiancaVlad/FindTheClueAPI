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
    public class GamesController : ApiController
    {
        private findthecluedbEntities db = new findthecluedbEntities();

        public GamesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Games
        public IQueryable<game> Getgames()
        {
            return db.games;
        }

        // GET: api/Games/5
        [ResponseType(typeof(game))]
        public IHttpActionResult Getgame(int id)
        {
            game game = db.games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgame(int id, game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.id_game)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gameExists(id))
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

        // POST: api/Games
        [ResponseType(typeof(game))]
        public IHttpActionResult Postgame(game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.games.Add(game);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game.id_game }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(game))]
        public IHttpActionResult Deletegame(int id)
        {
            game game = db.games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            db.games.Remove(game);
            db.SaveChanges();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gameExists(int id)
        {
            return db.games.Count(e => e.id_game == id) > 0;
        }
    }
}