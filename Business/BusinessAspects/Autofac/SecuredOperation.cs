using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Utilities.Interceptors.MethodInterceptionBaseAttribute;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        // JWT
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            // senın bekledıgın karektere goore ayırıp arraye atıyor
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            // istek yapılınca , her bır kısı ıcın httpcontext olusturuyor.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
