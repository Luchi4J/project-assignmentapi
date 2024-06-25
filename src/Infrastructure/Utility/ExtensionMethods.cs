using ProjectAssignment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssignment.Infrastructure.Utility
{
    public static class ExtensionMethods
    {
        public static JwtSecurityToken ExtractToken(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                var stream = str.Remove(0, 7);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var token = jsonToken as JwtSecurityToken;
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
    }
}
