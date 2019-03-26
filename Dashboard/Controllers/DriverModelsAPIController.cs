using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverModelsAPIController : ControllerBase
    {
        private readonly IDMDBContext _context;

        public DriverModelsAPIController(IDMDBContext context)
        {
            _context = context;
        }

        // GET: api/DriverModelsAPI
        [HttpGet]
        public IEnumerable<DriverModel> GetDriverModels()
        {
            return _context.DriverModels;
        }

        // GET: api/DriverModelsAPI/5
        /*
            I modified this standard rest api to allow for external ties from http sensors.
            You can use this api to "ping" to see whether a driver is showing as on.
            If the driver is on then you will get the driver object back, if the driver is "Stopped"
            then you will get a 404 error.
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driverModel = await _context.DriverModels.FindAsync(id);

            if (driverModel == null || driverModel.Status == "Stopped")
            {
                return NotFound();
            }

            return Ok(driverModel);
        }

        // PUT: api/DriverModelsAPI/5
        /*
            Added logic to track cache size.  When a new status comes in, the current cache will be
            stored as the previous cache allowing for you to see the trend.
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverModel([FromRoute] int id, [FromBody] DriverModel driverModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driverModel.DriverId)
            {
                return BadRequest();
            }

            var oldDriverModel = _context.DriverModels.Find(id);

            oldDriverModel.DriverName = driverModel.DriverName;
            oldDriverModel.PreviousCache = oldDriverModel.CurrentCache;
            oldDriverModel.CurrentCache = driverModel.CurrentCache - 72;
            oldDriverModel.Status = driverModel.Status;
            oldDriverModel.ServerId = driverModel.ServerId;

            if (oldDriverModel.Status == "Auto")
            {
                if (oldDriverModel.CurrentCache > oldDriverModel.PreviousCache)
                {
                    oldDriverModel.Status = "Rising";
                }
                else if (oldDriverModel.CurrentCache < oldDriverModel.PreviousCache)
                {
                    oldDriverModel.Status = "Lowering";
                }
                else
                {
                    oldDriverModel.Status = "Unchanged";
                }
            }

            _context.Update(oldDriverModel);
            //_context.Entry(driverModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DriverModelsAPI
        /*
            I commented out both the post and delete actions.  I instead am only doing these
            actions from the website itself under the DriverModel itself.  If you would prefer
            to do these using the API you can uncomment these sections, but the code is untested
            and is just the basic scaffolding
        */
        /*[HttpPost]
        public async Task<IActionResult> PostDriverModel([FromBody] DriverModel driverModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DriverModels.Add(driverModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverModel", new { id = driverModel.DriverId }, driverModel);
        }

        // DELETE: api/DriverModelsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driverModel = await _context.DriverModels.FindAsync(id);
            if (driverModel == null)
            {
                return NotFound();
            }

            _context.DriverModels.Remove(driverModel);
            await _context.SaveChangesAsync();

            return Ok(driverModel);
        }*/

        private bool DriverModelExists(int id)
        {
            return _context.DriverModels.Any(e => e.DriverId == id);
        }
    }
}