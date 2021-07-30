using System.Linq;
using System.Net;
using AutoMapper;
using crm.API.Mapping.Dtos;
using crm.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace crm.Controllers
{
    [ApiController]
    public class CurrenciesApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CurrenciesApiController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Method returns list of currencies.
        /// Requires authorization.
        /// </summary>
        /// Sample request:
        ///
        ///     {BaseUrl}/api/currencies
        ///
        /// Sample response:
        ///
        ///     {
        ///         "currencies": [
        ///             {
        ///                 "id": 1,
        ///                 "symbol": "PLN",
        ///                 "rate": 3.8750,
        ///                 "updated_at": "2021-07-24 10:45:02"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "symbol": "EUR",
        ///                 "rate": 4.5620,
        ///                 "updated_at": "2021-07-24 10:45:02"
        ///             }
        ///         ]
        ///     }
        /// 
        /// <returns>List of currencies</returns>
        [HttpGet]
        [Authorize(Policy = "Token")]
        [Route("api/currencies")]
        [SwaggerResponse((int) HttpStatusCode.OK, "OK", typeof(CurrenciesDto))]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, "Unauthorized", typeof(string))]
        public JsonResult GetCurrencies()
        {
            var currencies = _context.Currencies.Where(currency => !currency.Ghosted).ToList();
            var result = _mapper.Map<CurrenciesDto>(currencies);
            Response.StatusCode = (int) HttpStatusCode.OK;
            return Json(result);
        }
    }
}
