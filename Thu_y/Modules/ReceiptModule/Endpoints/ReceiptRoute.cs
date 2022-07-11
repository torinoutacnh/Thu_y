using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data.Entity;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
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
                    var receipts = receiptRepository.Get(x =>
                    model.Id == null ? true : x.Id == model.Id &&
                    model.IsEffect ? x.EffectiveDate < DateTimeOffset.UtcNow : true &&
                    model.CodeName == null ? true : x.CodeName.Contains(model.CodeName) &&
                    model.Name == null ? true : x.Name.Contains(model.Name) &&
                    model.CodeNumber == null ? true : x.CodeNumber.Contains(model.CodeNumber))
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
                     receiptService.CreateAsync(model);
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

            endpoints.MapPut(ReceiptEndpoint.UpdateReceipt, [Authorize(AuthenticationSchemes = "Bearer")] async (ReceiptModel model, IReceiptService receiptService) =>
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

            endpoints.MapDelete(ReceiptEndpoint.DeleteReceipt,[Authorize(AuthenticationSchemes = "Bearer")] async (string id, IReceiptService receiptService) =>
            {
                try
                {
                    await receiptService.DeleteAsync(id);
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
