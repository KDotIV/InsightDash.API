using System.Linq;
using InsightDash.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsightDash.API.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;
        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(server => server.id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult GetServer(int id)
        {
            var response = _ctx.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _ctx.Servers.Find(id);

            if(server == null) { return NotFound(); }

            // Refactor: move into a service
            if(msg.PayLoad == "activate")
            {
                server.isOnline = true;
                _ctx.SaveChanges();
            }

            if(msg.PayLoad == "deactivate")
            {
                server.isOnline = false;
            }

            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}