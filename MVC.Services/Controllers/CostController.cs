using Kendo.Mvc.UI;
using MVC.Data;
using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ModelBinding;
namespace MVC.Services.Controllers
{
   [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CostController : ApiController
    {
        AppDataContext db = new AppDataContext();
        [Route("api/Cost/GetGridData")]
       [HttpGet]        
        public IHttpActionResult GetGridData([ModelBinder(typeof(MVC.Services.Extensions.DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            var data = db.Costs.ToList();
            return Ok(data);
        }
        [Route("api/Cost/Save")]
        [HttpPost]
        public IHttpActionResult Save([FromBody] Cost cost)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cost.Id == 0)
            {
                db.Costs.Add(cost);
            }
            else
            {
                var task = db.Costs.Where(c => c.Id == cost.Id).FirstOrDefault();
                if (task != null)
                {
                    task.Name = cost.Name;
                    task.Priority = cost.Priority;
                    task.Description = cost.Description;
                    task.EstimatedCost = cost.EstimatedCost;
                }
            }
            db.SaveChanges();
            return Ok(cost);
        }
      [Route("api/Cost/Delete/{Id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            if(Id==0)
                return BadRequest("Cant Delete data");
            var cost=db.Costs.Where(c=>c.Id==Id).FirstOrDefault();
            if(cost==null)
                return BadRequest("Cant Delete data");
            db.Costs.Remove(cost);
            db.SaveChanges();
            return Ok("Data deleted successfully");
        }
      [Route("api/Cost/GetCost/{Id}")]
      [HttpGet]
      public IHttpActionResult GetCost(int? Id)
      {
          if (Id == 0)
              return BadRequest("Cant Delete data");
          var cost = db.Costs.Where(c => c.Id == Id).FirstOrDefault();
          if (cost == null)
              return BadRequest("Cant Delete data");
          else
              return Ok(cost);
      }
    }
}
