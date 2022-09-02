using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionManagerAPI.Models;
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace PermissionManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionTypeController : ControllerBase
    {
       

        private readonly ILogger<PermissionController> _logger;
        private readonly UserManagerContext userManager;

        public PermissionTypeController(ILogger<PermissionController> logger, UserManagerContext context)
        {
            _logger = logger;
            userManager = context;
        }

        [HttpGet(Name = "permissionTypes")]
        public async Task<ActionResult<IEnumerable<PermissionType>>> Get()
        {
            DbSet<PermissionType>? permissionTypes = userManager.PermissionTypes;
            if (permissionTypes is not null)
                return await permissionTypes.ToListAsync();

            return NoContent();
        }

        [HttpPost(Name = "permissionTypes")]
        public async Task<ActionResult<PermissionType>> Post([FromBody] PermissionType permissionType)
        {
            userManager.PermissionTypes?.Add(permissionType);
            await userManager.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = permissionType.Id}, permissionType);
        }

        [HttpGet("{id}")]
        [return: MaybeNull]
        public async Task<ActionResult<PermissionType>> Get(int id)
        {
            DbSet<PermissionType>? permissionTypes = userManager.PermissionTypes;
            if (permissionTypes is not null)
            {
                var permissionType = await permissionTypes.FindAsync(id);
                if (permissionType is null)
                    return NotFound();

                return Ok(permissionType);
            }

            return NoContent(); 
        }

        [HttpPut("{id}")]
        [return: MaybeNull]
        public async Task<IActionResult> Put(int id, PermissionType permissionType)
        {
            if (id != permissionType.Id)
                return BadRequest();

            userManager.Entry(permissionType).State = EntityState.Modified;

            try
            {
               await  userManager.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [return: MaybeNull]
        public async Task<IActionResult> Delete(int id)
        {
            var permissionType = userManager.PermissionTypes?.Find(id);

            if (permissionType == null)
                return NotFound();

            userManager.PermissionTypes?.Remove(permissionType);
            await userManager.SaveChangesAsync();

            return NoContent();
        }
    }
}