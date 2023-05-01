﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.DeleteBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries.GetAllBooks;
using Application.Features.BookFeatures.Queries.GetAllBooks_DataShaping;
using Application.Features.BookFeatures.Queries.GetBook;
using Application.Features.BookFeatures.Queries.GetBook_DataShaping;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// BookController
    /// </summary>
    public class BookController : BaseController
    {
        /// <summary>
        /// BookController constructor for initialisation ILogger
        /// </summary>
        /// <param name="loggerFactory"></param>
        public BookController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }


        /// <summary>
        /// Get list of Books
        /// </summary>
        /// <returns>Returns list of Books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllBookAsync([FromQuery] BookParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBooksQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]", 
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            return Ok(list);
        }

        /// <summary>
        /// Get Book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Returns entity by id</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetBookByIdQuery { Id = id }));
        }
        
        /// <summary>
        /// Craete new Book
        /// </summary>
        /// <param name="command">Cteate command</param>
        /// <returns>Returns id created entity</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBookAsync(CreateBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update Book by id
        /// </summary>
        /// <param name="command">Update command</param>
        /// <returns>Returns id updated entity</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Book by id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns>Return deleted entity id</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteBookCommand { Id = id }));
        }
 

        /// <summary>
        /// Get list of Book with selected fields
        /// </summary>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns list of Book with selected fields</returns>
        [HttpGet("datashaping/", Name = nameof(GetAllBook_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllBook_DataShapingAsync([FromQuery] BookParameter parameters)
        {
            var list = await Mediator.Send(new GetAllBooks_DataShapingQuery(parameters));
            
            _logger.LogInformation(
                "{Count} entities were successfully extracted from [{Table}]",
                list.Count(), 
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));
            
            
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }

        /// <summary>
        /// Get Book by id with selected fields
        /// </summary>
        /// <param name="id">Book id</param>
        /// <param name="parameters">Fields, that need select</param>
        /// <returns>Returns entity by id with selected fields</returns>
        [HttpGet("datashaping/{id:guid}", Name = nameof(GetBookById_DataShapingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBookById_DataShapingAsync
            (Guid id, [FromQuery] BookParameter parameters)
        {
            var entity = await Mediator.Send(new GetBookById_DataShapingQuery(parameters) { Id = id });
            
            _logger.LogInformation(
                "Entity were successfully extracted from [{Table}]",
                this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller", StringComparison.Ordinal)));

            
            string json = JsonSerializer.Serialize(entity, new JsonSerializerOptions
            {
                WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
            });
            
            return Ok(json);
        }
    }
}