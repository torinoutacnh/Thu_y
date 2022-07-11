using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReceiptModule.Endpoints
{
    public class ReceiptReportEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/receipt-report";
        public const string GetReceiptReport = BasePath;
        public const string CreateReceiptReport = BasePath + "/create";
        public const string UpdateReceiptReport = BasePath + "/update";
        public const string DeleteReceiptReport = BasePath + "/delete";
    }
    public static class ReceiptReportRoute
    {
        public static IEndpointRouteBuilder MapReceiptReportEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(ReceiptReportEndpoint.GetReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] (ReceiptReportPagingModel model, IReceiptReportService receiptReportService) =>
            {
                try
                {
                    var report = receiptReportService.GetReceiptReport(model);

                    return Results.Ok(value: new ResponseModel<ICollection<ReceiptReportQuarantineModel>>(data: report));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ICollection<ReceiptReportQuarantineModel>>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ICollection<ReceiptReportQuarantineModel>>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ReceiptReportEndpoint.BasePath);

            endpoints.MapPost(ReceiptReportEndpoint.CreateReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] async (ReceiptReportModel model, IReceiptReportService receiptReportService) =>
            {
                try
                {
                    await receiptReportService.CreateAsync(model);
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
            }).WithTags(ReceiptReportEndpoint.BasePath);

            endpoints.MapPost(ReceiptReportEndpoint.UpdateReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] async (ReceiptModel model, IReceiptReportService receiptReportService) =>
            {
                try
                {
                    await receiptReportService.UpdateAsync(model);
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
            }).WithTags(ReceiptReportEndpoint.BasePath);

            endpoints.MapPost(ReceiptReportEndpoint.DeleteReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, IReceiptReportService receiptReportService) =>
            {
                try
                {
                    await receiptReportService.DeleteAsync(id);
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

            }).WithTags(ReceiptReportEndpoint.BasePath);

            return endpoints;
        }
    }
}
