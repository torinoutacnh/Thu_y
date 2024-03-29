﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data.Entity;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReceiptModule.Endpoints
{
    public class ReceiptEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/receipt";
        public const string GetAllReceipt = BasePath;
        public const string CreateReceipt = BasePath + "/create-receipt";
        public const string UpdateReceipt = BasePath + "/update-receipt";
        public const string DeleteReceipt = BasePath + "/delete-receipt";
    }

    public static class ReceiptRoute
    {
        public static IEndpointRouteBuilder MapReceiptEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(ReceiptEndpoint.GetAllReceipt, [Authorize(AuthenticationSchemes = "Bearer")] (ReceiptPagingModel model, IReceiptRepository receiptRepository,IMapper mapper) =>
            {
                try
                {
                    var receipts = receiptRepository.Get(x => model.Id == null ? true : x.Id == model.Id)
                    .Include(x => x.Allocates)
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);

                    return Results.Ok(value: new ResponseModel<List<ReceiptModel>>(mapper.ProjectTo<ReceiptModel>(receipts).ToList()));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ReceiptEndpoint.BasePath);

            endpoints.MapPost(ReceiptEndpoint.CreateReceipt, [Authorize(AuthenticationSchemes = "Bearer")] async (ReceiptModel model, IReceiptService receiptService) =>
            {
                try
                {
                    await receiptService.CreateAsync(model);
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
            }).WithTags(ReceiptEndpoint.BasePath);

            endpoints.MapPost(ReceiptEndpoint.UpdateReceipt, [Authorize(AuthenticationSchemes = "Bearer")] async (UpdateReceiptModel model, IReceiptService receiptService) =>
            {
                try
                {
                    await receiptService.UpdateAsync(model);
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
            }).WithTags(ReceiptEndpoint.BasePath);

            endpoints.MapPost(ReceiptEndpoint.DeleteReceipt,[Authorize(AuthenticationSchemes = "Bearer")] async (DeleteModel request, IReceiptService receiptService) =>
            {
                try
                {
                    await receiptService.DeleteAsync(request.Id);
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

            }).WithTags(ReceiptEndpoint.BasePath);

            return endpoints;
        }
    }
}
