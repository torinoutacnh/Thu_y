using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReceiptModule.Endpoints
{
    public class AllocateReceiptEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/allocate-receipt";
        public const string GetAllReceipt = BasePath;
        public const string CreateReceipt = BasePath + "/create";
        public const string UpdateReceipt = BasePath + "/update";
        public const string DeleteReceipt = BasePath + "/delete";
    }

    public static class AllocateReceiptRoute
    {
        public static IEndpointRouteBuilder MapAllocateReceiptEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(AllocateReceiptEndpoint.GetAllReceipt, [Authorize(AuthenticationSchemes = "Bearer")] (AllocateReceiptPagingModel model, IReceiptAllocateRepository receiptAllocate, IMapper mapper ) =>
            {
                try
                {
                    var receipts = receiptAllocate.Get(x =>
                    model.Id == null ? true : x.Id == model.Id &&
                    model.UserId == null ? true : x.CodeName== model.UserId &&
                    model.ReceiptId == null ? true : x.ReceiptId == model.ReceiptId)
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);

                    return Results.Ok(value: new ResponseModel<List<ReceiptAllocateModel>>(mapper.ProjectTo<ReceiptAllocateModel>(receipts).ToList()));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(AllocateReceiptEndpoint.BasePath);

            endpoints.MapPost(AllocateReceiptEndpoint.CreateReceipt, [Authorize(AuthenticationSchemes = "Bearer")] (ReceiptAllocateModel model, IReceiptService receiptService) =>
            {
                try
                {
                    receiptService.AllocateReceipt(model);
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
            }).WithTags(AllocateReceiptEndpoint.BasePath);

            endpoints.MapPost(AllocateReceiptEndpoint.UpdateReceipt, [Authorize(AuthenticationSchemes = "Bearer")] (ReceiptAllocateModel model, IReceiptService receiptService) =>
            {
                try
                {
                    receiptService.UpdateAllocateReceipt(model);
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
            }).WithTags(AllocateReceiptEndpoint.BasePath);

            endpoints.MapPost(AllocateReceiptEndpoint.DeleteReceipt, [Authorize(AuthenticationSchemes = "Bearer")] (string id, IReceiptService receiptService) =>
            {
                try
                {
                    receiptService.DeleteAllocateReceipt(id);
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
            }).WithTags(AllocateReceiptEndpoint.BasePath);

            return endpoints;
        }
    }
}
