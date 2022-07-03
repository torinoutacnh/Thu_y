﻿using AutoMapper;
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
    }
    public static class FormRoute
    {
        public static IEndpointRouteBuilder MapFormEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(FormEndpoint.GetForm, async (string code, IFormRepository formRepository, IMapper mapper) =>
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
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            endpoints.MapPost(FormEndpoint.CreateForm, async (FormModel model, IFormService formService) =>
            {
                try
                {
                    formService.CreateForm(model);
                    return Results.Ok(value: new ResponseModel<string>("Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            endpoints.MapPut(FormEndpoint.UpdateForm, async (FormModel model, IFormService formService) =>
            {
                try
                {
                    formService.UpdateForm(model);
                    return Results.Ok(value: new ResponseModel<string>("Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(FormEndpoint.BasePath);

            return endpoints;
        }
    }
}