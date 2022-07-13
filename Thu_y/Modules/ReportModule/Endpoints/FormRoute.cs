﻿using AutoMapper;
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
        public const string GetTemplate = BasePath + "/template";

    }
    public static class FormRoute
    {
        public static IEndpointRouteBuilder MapFormEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(FormEndpoint.GetForm, [Authorize(AuthenticationSchemes = "Bearer")] async (string code, IFormRepository formRepository, IMapper mapper) =>
            {
                try
                {
                    var form = formRepository.Get(x => x.FormCode.ToLower() == code.ToLower() || x.Id.ToLower() == code.ToLower())
                                             .Include(x => x.FormAttributes)
                                             .FirstOrDefault();
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

            endpoints.MapPost(FormEndpoint.UpdateForm, [Authorize(AuthenticationSchemes = "Bearer")] async (FormModel model, IFormService formService) =>
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

            endpoints.MapPost(FormEndpoint.DeleteForm, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, IFormService formService) =>
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

            endpoints.MapGet(FormEndpoint.GetTemplate, [Authorize(AuthenticationSchemes = "Bearer")] (IFormRepository formRepository, IMapper mapper) =>
            {
                try
                {
                    var form = formRepository.Get().ToList();
                    if (form == null) throw new Exception("NoT found form!") { HResult = 400 };

                    return Results.Ok(value: new ResponseModel<ICollection<FormTemplateModel>>(mapper.Map<ICollection<FormTemplateModel>>(form)));
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
