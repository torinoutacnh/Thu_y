using Microsoft.AspNetCore.Authorization;
using Thu_y.Infrastructure.Utils.Constant;
using Thu_y.Modules.ReceiptModule.Model;
using Thu_y.Modules.ReceiptModule.Ports;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.UserModule.Core;
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
            endpoints.MapPost(ReceiptReportEndpoint.GetReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] (ReceiptReportPagingModel model, IReceiptReportService receiptReportService, IHttpContextAccessor httpContextAccessor) =>
            {
                try
                {
                    UserEntity userEntity = (UserEntity)httpContextAccessor.HttpContext.Items["UserEntity"];
                    if (userEntity == null) throw new Exception("Login Failed") { HResult = 500};
                    var report = userEntity.Role switch
                    {
                        RoleType.Manager => receiptReportService.GetReceiptReport(model),
                        _ => receiptReportService.GetReceiptReport(model, userEntity.Id)
                    };

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

            endpoints.MapPost(ReceiptReportEndpoint.UpdateReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] async (ReceiptReportModel model, IReceiptReportService receiptReportService) =>
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

            endpoints.MapPost(ReceiptReportEndpoint.DeleteReceiptReport, [Authorize(AuthenticationSchemes = "Bearer")] async (DeleteModel request, IReceiptReportService receiptReportService) =>
            {
                try
                {
                    await receiptReportService.DeleteAsync(request.Id);
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
