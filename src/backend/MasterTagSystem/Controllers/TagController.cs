using Microsoft.AspNetCore.Mvc;
using MasterTagSystem.Models;
using MasterTagSystem.Services;

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
        public IActionResult ValidateTag([FromBody] Tag tag)
        {
            var isValid = _tagService.ValidateTag(tag);
            return Ok(new { isValid });
        }
    }
}
