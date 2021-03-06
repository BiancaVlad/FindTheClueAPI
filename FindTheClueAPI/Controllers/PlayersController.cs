﻿using System;
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
    public class PlayersController : ApiController
    {
        private findthecluedbEntities db = new findthecluedbEntities();

        public PlayersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Players
        public IQueryable<player> Getplayers()
        {
            return db.players;
        }

        // GET: api/Players/5
        [ResponseType(typeof(player))]
        public IHttpActionResult Getplayer(int id)
        {
            player player = db.players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putplayer(int id, player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.id_player)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!playerExists(id))
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

        // POST: api/Players
        [ResponseType(typeof(player))]
        public IHttpActionResult Postplayer(player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.players.Add(player);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = player.id_player }, player);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(player))]
        public IHttpActionResult Deleteplayer(int id)
        {
            player player = db.players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            db.players.Remove(player);
            db.SaveChanges();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool playerExists(int id)
        {
            return db.players.Count(e => e.id_player == id) > 0;
        }
    }
}