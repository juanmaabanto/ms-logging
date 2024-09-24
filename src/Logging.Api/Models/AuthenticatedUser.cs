using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace Sofisoft.Enterprise.Logging.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class AuthenticatedUser : IIdentity
    {
        public AuthenticatedUser(string authenticationType, bool isAuthenticated, string name)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }
    
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get;}
        public string Name { get; }

    }
}