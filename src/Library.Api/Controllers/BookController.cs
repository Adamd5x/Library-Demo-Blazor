using ErrorOr;
using Library.Abstracts.Core;
using Library.Api.Validators;
using Library.Core.Extensions;
using Library.Models;
using Library.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route ("api/book")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BookController(IBookCoreService coreService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetBook ([FromRoute]int id)
        {
            bool isIdInvalid = id <= 0;
            if (isIdInvalid) 
            {
                return BadRequest ();
            }

            var result = await coreService.GetAsync(id);
            if (result.IsError) 
            {
                if (result.FirstError.Type == ErrorType.NotFound) 
                { 
                    return NotFound();
                }
                return BadRequest (GetProblemDetails (result.FirstError));
            }
            return Ok (result.Value);
        }

        [HttpGet ("books")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookDto>))]
        [ProducesResponseType (StatusCodes.Status400BadRequest, Type = typeof (ProblemDetails))]
        [ProducesResponseType (StatusCodes.Status404NotFound, Type = typeof (ProblemDetails))]
        public async Task<IActionResult> GetBooks (string sortBy = "Title", string sortOrder = "Ascending", int offset = 0, int size = 50) {

            bool isSortColumnValid = SortColumnValidator.Validate (sortBy);
            bool isPagingValid = offset >= 0 && size >=0 && size <= 100;
            
            if (!isSortColumnValid && !isPagingValid)
            {
                return BadRequest ();
            }

            var result = await coreService.GetAllAsync(sortBy, sortOrder.ToEnum<SortOrder>(), offset, size);
            if (result.IsError) 
            { 
                return BadRequest (GetProblemDetails (result.FirstError));
            }

            return Ok (result.Value);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Create ([FromBody] BookDto model)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest ();
            }

            var result = await coreService.CreateAsync (model);

            if (result.IsError) 
            {
                return BadRequest (GetProblemDetails (result.FirstError));
            }

            return Created ("", model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (IEnumerable<BookDto>))]
        [ProducesResponseType (StatusCodes.Status400BadRequest, Type = typeof (ProblemDetails))]
        [ProducesResponseType (StatusCodes.Status404NotFound, Type = typeof (ProblemDetails))]
        public async Task<IActionResult> Update ([FromRoute]int id, [FromBody] BookDto model)
        {
            bool isIdInvalid = id <= 0;

            if (!ModelState.IsValid && isIdInvalid) 
            {
                return BadRequest ();
            }

            var result = await coreService.UpdateAsync (id, model);
            if (result.IsError)
            {
                return BadRequest (GetProblemDetails (result.FirstError));
            }
            return Ok (result.Value);
        }


        [HttpPut ("chstate/{id}/{state}")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (IEnumerable<BookDto>))]
        [ProducesResponseType (StatusCodes.Status400BadRequest, Type = typeof (ProblemDetails))]
        [ProducesResponseType (StatusCodes.Status404NotFound, Type = typeof (ProblemDetails))]
        public async Task<IActionResult> ChangeState ([FromRoute] int id, [FromRoute] string state)
        {
            var result = await coreService.SetState(id, state);
            if (result.IsError)
            {
                return BadRequest (GetProblemDetails (result.FirstError));
            }
            return Ok (result.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType (StatusCodes.Status400BadRequest, Type = typeof (ProblemDetails))]
        public async Task<IActionResult> Delete ([FromRoute] int id)
        {
            bool isIdInvalid = id <= 0;
            if (isIdInvalid) 
            { 
                return BadRequest ();
            }

            var result = await coreService.DeleteAsync (id);
            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorType.NotFound)
                {
                    return NotFound ();
                }
                return BadRequest (GetProblemDetails(result.FirstError));
            }
            return Ok ();
        }

        private static ProblemDetails GetProblemDetails(Error error)
        {
            return  new ProblemDetails ()
            {
                Title = "API Error",
                Detail = error.Description,
                Status = StatusCodes.Status400BadRequest,
                Instance = "API"
            };
        }
    }
}
