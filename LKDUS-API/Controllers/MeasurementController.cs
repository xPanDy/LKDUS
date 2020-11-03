using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LKDUS_API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LKDUS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementRepository measurementRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MeasurementController(IMeasurementRepository measurementRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.measurementRepository = measurementRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
