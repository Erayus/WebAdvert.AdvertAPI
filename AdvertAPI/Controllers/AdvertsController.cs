using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using AdvertAPI.Models;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace AdvertAPI.Controllers
{
    
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public AdvertsController(IAdvertStorageService advertStorageService, IConfiguration configuration)
        {
            _advertStorageService = advertStorageService;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel advert)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.AddAsync(advert);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return StatusCode(201, new CreateAdvertResponse {Id = recordId});
        }


        
        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel confirmAdvert)
        {
            try
            {
                await _advertStorageService.ConfirmAsync(confirmAdvert);
                // await RaiseAdvertConfirmedMessage(confirmAdvert);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return new OkResult();
        }
    }
}