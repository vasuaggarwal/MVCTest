using Kendo.Mvc.UI;
using MVC.Data;
using MVC.Data.Infrastructure;
using MVC.Data.Repositories;
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
        private ICostRepository costRepository;
        private IUnitOfWork unitOfWork;
        public CostController(ICostRepository costRepository, IUnitOfWork unitOfWork)
        {
            this.costRepository = costRepository;
            this.unitOfWork = unitOfWork;
           
        }
        [Route("api/Cost/GetGridData")]
       [HttpGet]        
        public IHttpActionResult GetGridData([ModelBinder(typeof(MVC.Services.Extensions.DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            var data = costRepository.GetAll().ToList();
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
                costRepository.Add(cost);  
            }
            else
            {
                var task = costRepository.GetById(cost.Id);
                if (task != null)
                {
                    task.Name = cost.Name;
                    task.Priority = cost.Priority;
                    task.Description = cost.Description;
                    task.EstimatedCost = cost.EstimatedCost;
                    costRepository.Update(task);
                }
            }
            unitOfWork.Commit();
            return Ok(cost);
        }
      [Route("api/Cost/Delete/{Id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            if(Id==0)
                return BadRequest("Cant Delete data");
            var cost = costRepository.GetById(Id);
            if(cost==null)
                return BadRequest("Cant Delete data");
            costRepository.Delete(cost);
            unitOfWork.Commit();
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
