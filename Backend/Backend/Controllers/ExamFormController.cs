using Backend.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse_Management_System.Repository;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamFormController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;

        public ExamFormController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Produces("application/json")]
        public IActionResult AddExam(ExaminationForm form)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ExamFormRepository.add(form);
                unitOfWork.save();
                return Ok(form);
            }
            return BadRequest();
        }
    }
}
