using Microsoft.AspNetCore.Mvc;
using MasterTagSystem.Models;
using MasterTagSystem.Services;

namespace MasterTagSystem.Controllers
{
    /// <summary>
    /// Controller for handling tag-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="tagService">The tag service.</param>
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Validates the given tag.
        /// </summary>
        /// <param name="tag">The tag to validate.</param>
        /// <returns>An IActionResult containing the validation result.</returns>
        [HttpPost("validate")]
        public IActionResult ValidateTag([FromBody] TagModel tag)
        {
            var isValid = _tagService.ValidateTag(tag); // Validates and adds to MongoDB
            return Ok(new { isValid });
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>An IActionResult containing all tags.</returns>
        [HttpGet("tags")]
        public IActionResult GetAllTags()
        {
            var tags = _tagService.GetAllTags(); // Retrieves all JSON tags from MongoDB
            return Ok(tags);
        }

        /// <summary>
        /// Updates the tag with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the tag to update.</param>
        /// <param name="updatedTag">The updated tag data.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        [HttpPut("update/{id}")]
        public IActionResult UpdateTag(string id, [FromBody] TagModel updatedTag)
        {
            var isUpdated = _tagService.UpdateTag(id, updatedTag);
            if (isUpdated)
            {
                return Ok(new { message = "Tag updated successfully." });
            }
            else
            {
                return NotFound(new { message = "Tag not found or not updated." });
            }
        }
    }
}
