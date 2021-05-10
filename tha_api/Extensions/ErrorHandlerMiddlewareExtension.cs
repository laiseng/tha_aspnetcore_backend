using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Linq;

using System;
using System.Net;
using THA.Model.Core;
using System.IdentityModel.Tokens.Jwt;

namespace THA_Api.Extensions
{
   public static class ErrorHandlerMiddlewareExtension
   {
      public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILogger logger)
      {
         app.UseExceptionHandler(appError =>
         {
            appError.Run(async context =>
            {
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               context.Response.ContentType = "application/json";

               var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
               if (contextFeature != null)
               {
                  var errorInfo = new ErrorInfoModel()
                  {
                     ErrorId = Guid.NewGuid(),
                     SessionId = context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti).FirstOrDefault().Value,
                     StatusCode = context.Response.StatusCode,
                     Message = "Internal Server Error."
                  }.ToString();

                  logger.Error($"THA_Exception: {contextFeature.Error} Info: {errorInfo}");
                  await context.Response.WriteAsync(errorInfo); ;
               }
            });
         });
      }
   }
}