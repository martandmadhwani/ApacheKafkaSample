using ApacheKafkaSampleNetCore.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ApacheKafkaSampleNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }

        [HttpPost("Send")]
        public async Task<ActionResult> Get(string topic,[FromBody]Employee employee)
        {
            string serializeemployee = JsonConvert.SerializeObject(employee);
            using (var producer = new ProducerBuilder<Null,string>(_config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializeemployee });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}
