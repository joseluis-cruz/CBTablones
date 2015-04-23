﻿using System.Collections.Generic;
using System.Web.Http;

namespace ApiRestServerExample.Controllers
{
  [Authorize] 
  public class ExampleController : ApiController
  {
    //Este es el controlador de ejemplo no accede a la capa de datos


    // GET: api/Example
    public IEnumerable<string> Get()
    {
      return new[] {"value1", "value2"};
    }

    // GET: api/Example/5
     
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Example
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/Example/5
    public void Put(int id, [FromBody] string value)
    {

    }

    // DELETE: api/Example/5
    public void Delete(int id)
    {

    }
  }
}