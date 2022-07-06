using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReportModule.Endpoints
{
    public class FormEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/form";
        public const string GetForm = BasePath;
        public const string CreateForm = BasePath + "/create-form";
        public const string UpdateForm = BasePath + "/update-form";
        public const string DeleteForm = BasePath + "/delete-form";
    }
    public static class FormRoute
    {
        public static IEndpointRouteBuilder MapFormEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(FormEndpoint.GetForm, [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")] async (string code, IFormRepository formRepository, IMapper mapper) =>
            {
                try
                {
                    var form = formRepository.Get(x => x.FormCode.ToLower().Equals(code.ToLower()) || x.Id.ToLower().Equals(code.ToLower())).Include(x => x.FormAttributes).FirstOrDefault();
                    if (form == null) throw new Exception("NoT found form!") { HResult = 400 };

                    return Results.Ok(value: new ResponseModel<FormModel>(mapper.Map<FormModel>(form)));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(FormEndpoint.BasePath);

            endpoints.MapPost(FormEndpoint.CreateForm,[Authorize(AuthenticationSchemes = "Bearer")] async (FormModel model, IFormService formService) =>
            {
                try
                {
                    formService.CreateForm(model);
                    return Results.Ok(value: new ResponseModel<string>(data:"Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            endpoints.MapPut(FormEndpoint.UpdateForm, [Authorize(AuthenticationSchemes = "Bearer")] async (FormModel model, IFormService formService) =>
            {
                try
                {
                    formService.UpdateForm(model);
                    return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            endpoints.MapDelete(FormEndpoint.DeleteForm, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, IFormService formService) =>
            {
                try
                {
                    formService.DeleteForm(id);
                    return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            return endpoints;
        }
    }
}
