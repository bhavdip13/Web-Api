using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Demo_WebApi.Models;

namespace Demo_WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private PMSEntities1 db = new PMSEntities1();

        // GET api/Product
        public IEnumerable<Mst_Product> GetMst_Product()
        {
            return db.Mst_Product.AsEnumerable();
        }

        // GET api/Product/5
        public Mst_Product GetMst_Product(int id)
        {
            Mst_Product mst_product = db.Mst_Product.Find(id);
            if (mst_product == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return mst_product;
        }

        // PUT api/Product/5
        //PUT thant means Update the recored.
        public HttpResponseMessage PutMst_Product(int id, Mst_Product mst_product)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != mst_product.PID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(mst_product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Product
        //Post thant means Inset recored.
        public HttpResponseMessage PostMst_Product(Mst_Product mst_product)
        {
            if (ModelState.IsValid)
            {
                db.Mst_Product.Add(mst_product);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mst_product);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = mst_product.PID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Product/5
        public HttpResponseMessage DeleteMst_Product(int id)
        {
            Mst_Product mst_product = db.Mst_Product.Find(id);
            if (mst_product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Mst_Product.Remove(mst_product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, mst_product);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}