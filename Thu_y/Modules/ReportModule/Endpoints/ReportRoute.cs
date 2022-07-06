using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReportModule.Endpoints
{
    public class ReportEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/report";
        public const string GetAllReport = BasePath;
        public const string CreateReport = BasePath + "/create-report";
        public const string UpdateReport = BasePath + "/update-report";
        public const string DeleteReport = BasePath + "/delete-report";
    }

    public static class ReportRoute
    {
        public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(ReportEndpoint.GetAllReport,async ([FromBody]ReportPagingModel model, IReportTicketRepository reportRepository,IMapper mapper) =>
            {
                try
                {
                    var reports = reportRepository.Get(x => 
                    model.Id == null ? true : x.Id.Equals(model.Id)
                    && x.DateCreated >= (model.DateStart ?? DateTimeOffset.MinValue)
                    && x.DateCreated <= (model.DateEnd ?? DateTimeOffset.MaxValue)
                    && model.Type == null ? true : (int)x.Type == model.Type
                    && model.UserId == null ? true : x.UserId.Equals(model.UserId))
                    .Include(x => x.Values)
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);

                    return Results.Ok(value: new ResponseModel<List<ReportModel>>(mapper.ProjectTo<ReportModel>(reports).ToList()));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapPost(ReportEndpoint.CreateReport, async (ReportModel model, IReportService reportService) =>
            {
                try
                {
                    reportService.CreateReport(model);
                    return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message),statusCode:ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapPut(ReportEndpoint.UpdateReport, async (ReportModel model, IReportService reportService) =>
            {
                try
                {
                    reportService.UpdateReport(model);
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
            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapDelete(ReportEndpoint.DeleteReport, async (string id, IReportService reportService) =>
            {
                try
                {
                    reportService.DeleteReport(id);
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

            }).WithTags(ReportEndpoint.BasePath);

            return endpoints;
        }
    }
}
