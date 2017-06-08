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
    public class QuestionsController : ApiController
    {
        private findthecluedbEntities db = new findthecluedbEntities();

        public QuestionsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Questions
        public IQueryable<question> Getquestions()
        {
            return db.questions;
        }

        // GET: api/Questions/5
        [ResponseType(typeof(question))]
        public IHttpActionResult Getquestion(int id)
        {
            question question = db.questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putquestion(int id, question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.id_question)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!questionExists(id))
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

        // POST: api/Questions
        [ResponseType(typeof(question))]
        public IHttpActionResult Postquestion(question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.questions.Add(question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = question.id_question }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(question))]
        public IHttpActionResult Deletequestion(int id)
        {
            question question = db.questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool questionExists(int id)
        {
            return db.questions.Count(e => e.id_question == id) > 0;
        }
    }
}