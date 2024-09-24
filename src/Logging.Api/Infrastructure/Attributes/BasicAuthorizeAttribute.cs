using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace Sofisoft.Enterprise.Logging.Api.Infrastructure.Attributes
{
    [ExcludeFromCodeCoverage]
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public BasicAuthorizeAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
    
}