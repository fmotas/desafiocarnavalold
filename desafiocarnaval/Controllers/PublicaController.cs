using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace desafiocarnaval.Controllers
{
    [Produces("application/json")]
    [Route("api/Publica")]
    public class PublicaController : Controller
    {
	    IModel model = Program._model;
		IBasicProperties basicProperties = Program._basicProperties;

	    //public PublicaController(IModel _model, IBasicProperties _basicProperties)
	    //{
		   // model = Program._model;
		   // basicProperties = Program._basicProperties;
	    //}

        // GET: api/Publica
        [HttpGet]
        public void Get()
        {
        }

        // GET: api/Publica/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Publica
        [HttpPost]
        public void Post([FromBody]string value)
        {
	        byte[] mensagem = Encoding.UTF8.GetBytes(value);
	        model.BasicPublish("exchangeDesafio",Request.Headers["RoutingKey"],basicProperties, mensagem);
        }
        
        // PUT: api/Publica/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
