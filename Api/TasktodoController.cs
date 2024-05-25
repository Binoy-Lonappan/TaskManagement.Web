using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagement.Web.Data.Models;
using TaskManagement.Web.Data.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasktodoController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITaskToDoProvider _taskToDoProvider;


        public TasktodoController(IWebHostEnvironment webHostEnvironment, ITaskToDoProvider taskToDoProvider)
        {
            _webHostEnvironment = webHostEnvironment;
            _taskToDoProvider = taskToDoProvider;
        }

        // GET: api/<TasktodoController>
        [HttpGet]
        public JsonResult Get()
        {
            var result = new JsonResult(_taskToDoProvider.GetTasksToDo());
            return result;
        }

        // GET api/<TasktodoController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var completerecords = _taskToDoProvider.GetTasksToDo();
            var cydata = completerecords
               .Where(p => p.DueDate.Year == DateTime.Now.Year)
               .GroupBy(p => p.Status)
               .Select(g => new { Status = g.Key, C_ount = g.Count() })
               .ToList();

            var tdata = completerecords
               .GroupBy(p => p.Status)
               .Select(g => new { Status = g.Key, C_ount = g.Count() })
               .ToList();

            var result = new JsonResult(new { cydata = cydata, tdata = tdata });

            return result;
        }

        // POST api/<TasktodoController>
        [HttpPost]
        public JsonResult Post([FromBody] TasksToDo t)  //[FromBody] TasksToDo t  //TasksToDo t = new TasksToDo();
        {

            string msg = string.Empty;
            var result = _taskToDoProvider.SaveTaskToDo(t, out msg);
            return new JsonResult(new { success = result, message = msg });
        }

        // PUT api/<TasktodoController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public JsonResult Put([FromBody] TasksToDo t)
        {
            string msg = string.Empty;
            var result = _taskToDoProvider.UpdateTaskToDo(t, out msg);
            return new JsonResult(new { success = result, message = msg });
        }

        // DELETE api/<TasktodoController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string msg = string.Empty;
            var result = _taskToDoProvider.DeleteTaskToDo(id, out msg);
            return new JsonResult(new { success = result, message = msg });
        }
    }
}
