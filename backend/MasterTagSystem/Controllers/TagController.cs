using Microsoft.AspNetCore.Mvc;
using MasterTagSystem.Models;

namespace MasterTagSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("validate")]
        public IActionResult ValidateTag([FromBody] TagModel tag)
        {
            var isValid = _tagService.ValidateTag(tag); // Valide et ajoute dans MongoDB
            return Ok(new { isValid });
        }
    }
}
