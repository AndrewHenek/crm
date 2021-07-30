using System.Linq;
using System.Net;
using AutoMapper;
using crm.API.Mapping.Dtos;
using crm.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Authorize(Policy = "Token")]
        [Route("api/Currencies")]
        public JsonResult GetCurrencies()
        {
            var currencies = _context.Currencies.Where(currency => !currency.Ghosted).ToList();
            var result = _mapper.Map<CurrenciesDto>(currencies);
            Response.StatusCode = (int) HttpStatusCode.OK;
            return Json(result);
        }
    }
}
