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
        public IActionResult ValidateTag([FromBody] TagModel tag)
        {
            var isValid = _tagService.ValidateTag(tag); // Valide et ajoute dans MongoDB
            return Ok(new { isValid });
        }

        // Nouvelle méthode GET pour récupérer tous les JSONs
        [HttpGet("tags")]
        public IActionResult GetAllTags()
        {
            var tags = _tagService.GetAllTags(); // Récupère tous les tags JSONs depuis MongoDB
            return Ok(tags);
        }

        // Nouveau endpoint pour mettre à jour un tag
        [HttpPut("update/{id}")]
        public IActionResult UpdateTag(string id, [FromBody] TagModel updatedTag)
        {
            var isUpdated = _tagService.UpdateTag(id, updatedTag);
            if (isUpdated)
            {
                return Ok(new { message = "Tag mis à jour avec succès." });
            }
            else
            {
                return NotFound(new { message = "Tag non trouvé ou non mis à jour." });
            }
        }
    }
}
