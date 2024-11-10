using Asp.Versioning;
using Core.Common.Models;
using Core.Domain.Classes;
using Core.Domain.Dto;
using Core.UseCase.V1.OrderOperations.Commands.Create;
using Core.UseCase.V1.OrderOperations.Commands.Delete;
using Core.UseCase.V1.OrderOperations.Commands.Update;
using Core.UseCase.V1.OrderOperations.Queries.GetAll;
using Core.UseCase.V1.OrderOperations.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using PruebaApi.Helpers;

namespace PruebaApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : ApiControllerBase
    {
        /// <summary>
        /// Creación de una order de inversión
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateOrderCommand body) => Result(await Sender.Send(body));

        /// Listado de Ordenes por filtros y paginados
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] char? Operation, int? state, int? page, int? size) => Result(await Sender.Send(new GetOrdersByFilters()
        {
            Operation = Operation,
            State=state,
            Page = page ?? 1,
            Size = size ?? 10
        }));

        /// Obtiene una Orden por id
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Result(await Sender.Send(new GetOrderById { Id = id }));
        
        /// Actualiza el estado de una Orden por id
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,int state) => Result(await Sender.Send(new UpdateOrderStatusCommand { Id = id,State=state }));
        
        /// Elimina una Orden por id
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Result(await Sender.Send(new DeleteOrderCommand { Id = id }));
    
    }
}
