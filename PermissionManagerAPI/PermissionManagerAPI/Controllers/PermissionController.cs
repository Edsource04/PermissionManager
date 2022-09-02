using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionManagerAPI.Models;
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace PermissionManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
       

        private readonly ILogger<PermissionController> _logger;
        private readonly UserManagerContext userManager;

        public PermissionController(ILogger<PermissionController> logger, UserManagerContext context)
        {
            _logger = logger;
            userManager = context;
        }

        [HttpGet(Name = "Permissions")]
        public async Task<ActionResult<IEnumerable<Permission>>> Get()
        {
            DbSet<Permission>? permissions = userManager.Permissions;
            if (permissions is not null)
            {
                var pers = await permissions.Include(c => c.PermissionType).ToListAsync();
                return pers;
            }
            return NoContent();
        }

        [HttpPost(Name = "Permissions")]
        public async Task<ActionResult<Permission>> Post([FromBody] Permission permission)
        {
            userManager.Permissions?.Add(permission);
            await userManager.SaveChangesAsync();

            return CreatedAtAction(nameof(GetId), new { id = permission.Id}, permission);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetId(int id)
        {
            DbSet<Permission>? permissions = userManager.Permissions;

            if (permissions is not null)
            {
                var permission = await permissions.FindAsync(id);
                if (permission is null)
                    return NotFound();

                return permission;
            }

            return NotFound();  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Permission permission)
        {
            if (id != permission.Id)
                return BadRequest();

            userManager.Entry(permission).State = EntityState.Modified;

            try
            {
                await userManager.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
               var ifFound =   userManager.Permissions?.Find(id);
                if (ifFound is null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DbSet<Permission>? permissions = userManager.Permissions;
            if(permissions is not null)
            {
                var per = await permissions.FindAsync();
                if (per == null)
                    return NotFound();

                permissions.Remove(per);
                await userManager.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest();
        }
    }
}