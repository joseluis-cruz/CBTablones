using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ApiRestServerExample.Models;

namespace ApiRestServerExample.BasicAuth
{
  public class BasicAuthMessageHandler : DelegatingHandler
  {
    //http://geeks.ms/blogs/etomas/archive/2013/02/20/como-hacer-seguros-tus-servicios-webapi.aspx

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var headers = request.Headers;

      if (headers.Authorization == null || headers.Authorization.Scheme != "Basic")
        return base.SendAsync(request, cancellationToken);

      //Obtenemos las credenciales
      var currentUser = getCredentials(headers.Authorization.Parameter);

      //Validamos al usuario
      if (!isAuthorizatedUser(currentUser)) return base.SendAsync(request, cancellationToken);

      //Usuario autorizado
      var principal = new GenericPrincipal(new GenericIdentity(currentUser.Name), null);
      PutPrincipal(principal);

      return base.SendAsync(request, cancellationToken);
    }

    private bool isAuthorizatedUser(UserPwd currentUser)
    {
      using (var dbContext = new TestDatabaseEntities1())
      {
        try
        {
          var dbUser = dbContext.UserPwds.First(x => x.Name == currentUser.Name);

          if (dbUser.Password == currentUser.Password)
          {
            return true;
          }
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      return false;
    }
    private void PutPrincipal(IPrincipal principal)
    {
      Thread.CurrentPrincipal = principal;
      if (HttpContext.Current != null)
      {
        HttpContext.Current.User = principal;
      }
    }
    private UserPwd getCredentials(string headerAuthorization)
    {
      var userPwd = Encoding.UTF8.GetString(Convert.FromBase64String(headerAuthorization));
      var user = userPwd.Substring(0, userPwd.IndexOf(':'));
      var password = userPwd.Substring(userPwd.IndexOf(':') + 1);

      var credential = new UserPwd
      {
        Name = user,
        Password = password
      };
      return credential;
    }
  }
}