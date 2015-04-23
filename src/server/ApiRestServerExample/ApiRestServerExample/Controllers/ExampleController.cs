using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;

namespace ApiRestServerExample.Controllers
{
  [Authorize] 
  public class ExampleController : ApiController
  {
    //Este es el controlador de ejemplo no accede a la capa de datos
    // GET: api/Example
    public string Get()
    {
      using (var dbContext = new TestDatabaseEntities1())
      {
        var messagesList = dbContext.Messages.ToList();
        var serializedList = serializer(messagesList);
        return serializedList;
      }
    }

    // GET: api/Example/5
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Example
    public void Post([FromBody] string value)
    {
      Console.WriteLine(value);
    }

    // PUT: api/Example/5
    public void Put(int id, [FromBody] string value)
    {

    }

    // DELETE: api/Example/5
    public void Delete(int id)
    {

    }


    private string serializer(object o)
    {
      return JsonConvert.SerializeObject(o, Formatting.None);
    }
  }
}