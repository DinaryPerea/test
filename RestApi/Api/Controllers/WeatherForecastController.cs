using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<long> Get(int testcases, int x, int y, int z, int x1, int x2, int y1, int y2,
            int z1, int z2, int dimension, int value)
        {
            Entidades.InfoInput info = new Entidades.InfoInput();
            info.testcases = testcases;
            info.x = x;
            info.y = y;
            info.z = z;
            info.x1 = x1;
            info.x2 = x2;
            info.y1 = y1;
            info.y2 = y2;
            info.z1 = z1;
            info.z2 = z2;
            info.dimension = dimension;
            //info.typeOperation = typeOperation;
            List<long> res = new List<long>();
            res = prueba(info);
            return res;

        }

        /// <summary>
        /// Metodo que retorna la informacion de la actualizacion y las sumatorias.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        private static List<long> prueba(Entidades.InfoInput info)
        {
            List<long> res = new List<long>();
            int testcases = info.testcases;
            for (int i = 0; i < testcases; i++)
            {
                int numOperations = 1;
                for (int j = 0; j < numOperations; j++)
                {
                    var updateResult = Logica.Negocio.update(info.x, info.y, info.z, info.value, info.dimension);
                    res.Add(updateResult);
                    var sumResult = Logica.Negocio.query(info.x1, info.x2, info.y1, info.y2, info.z1, info.z2);
                    res.Add(sumResult);
                       
                }
            }
            return res;
        }

    }
}
