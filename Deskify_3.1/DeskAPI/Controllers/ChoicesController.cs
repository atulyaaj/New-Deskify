using DeskBusiness.Services;
using DeskEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoicesController : ControllerBase
    {
        private ChoicesService _choicesService;
        public ChoicesController(ChoicesService choicesService)
        {
            _choicesService = choicesService;
        }

        [HttpPost("AddChoice")]
        public IActionResult AddChoice(Choices choice)
        {
            _choicesService.AddChoice(choice);
            return Ok("Choices Added Successfully");

        }

        [HttpDelete("DeleteChoice")]
        public IActionResult DeleteChoice(int choiceId)
        {
            _choicesService.DeleteChoice(choiceId);
            return Ok("Choices Deleted Successfully");
        }

        [HttpPut("UpdateChoice")]
        public IActionResult UpdateChoice(Choices choice)
        {
            _choicesService.UpdateChoice(choice);
            return Ok("Choices Updated Successfully");
        }

        [HttpGet("GetAllChoices")]
        public IEnumerable<Choices> GetAllChoices()
        {
            return _choicesService.GetAllChoices();
        }

        [HttpGet("GetChoiceById")]
        public Choices GetChoiceById(int choiceId)
        {
            return _choicesService.GetChoiceById(choiceId);
        }
    }
}
