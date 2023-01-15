﻿using AutoMapper;
using DnD.Api.CustomAttributes;
using DnD.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKeyAuth]
    public class ClassController : ControllerBase
    {
        private readonly ClassRepository _classRepository;
        private readonly ILogger<ClassController> _logger;
        private readonly IMapper _mapper;
        public ClassController(ILogger<ClassController> logger, ClassRepository classRepository, IMapper mapper)
        {
            _logger = logger;
            _classRepository = classRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _classRepository.GetAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.ClassModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _classRepository.GetByIdAsync(id, cancellationToken);
                var response = _mapper.Map<Shared.Models.ClassModel>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{categoryId}/category")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var responseRepo = await _classRepository.GetByCategoryIdAsync(categoryId, cancellationToken);
                var response = _mapper.Map<IEnumerable<Shared.Models.ClassModel>>(responseRepo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
