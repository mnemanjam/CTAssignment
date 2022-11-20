using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace CT.Models
{
    public static class UserPrincipal
    {
        

        //public static String GetCRMS(this IPrincipal principal)
        //{
        //    var claimsPrincipal = principal as ClaimsPrincipal;
        //    if (claimsPrincipal == null)
        //    {
        //        throw new CannotUnloadAppDomainException("User is not authenticated");
        //    }

        //    var personNameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "CT:CRMS");
        //    if (personNameClaim != null)
        //    {
        //        return personNameClaim.Value;
        //    }

        //    return String.Empty;
        //}
    }
}