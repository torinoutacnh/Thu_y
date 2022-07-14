using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        public const string GetKillReport = BasePath + "/animal-killing";
        public const string ListAbattoirReport = BasePath + "/list-abattoir";
        public const string ListQuarantineReport = BasePath + "/list-quarantine";
        public const string RevenueQuarantineReport = BasePath + "/revenue-quarantine";
        public const string GetSingleReport = BasePath + "/single-report";

    }
    [EnableCors("LongPolicy")]
    public static class ReportRoute
    {
        public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(ReportEndpoint.GetAllReport, [Authorize(AuthenticationSchemes = "Bearer")] async ([FromBody] ReportPagingModel model, IReportTicketRepository reportRepository, IMapper mapper) =>
            {
                try
                {
                    
                    var reports = new List<Core.ReportTicketEntity>();
                    if (string.IsNullOrEmpty(model.UserId))
                    {
                        reports = reportRepository.Get(x => x.FormId == model.FormId, true, y => y.Values)
                                                  .OrderByDescending(x => x.DateCreated)
                                                  .Skip(model.PageNumber * model.PageSize)
                                                  .Take(model.PageSize).ToList();
                        return Results.Ok(value: new ResponseModel<List<ReportModel>>(mapper.Map<List<ReportModel>>(reports)));

                    }
                    reports = reportRepository.Get(x => x.UserId == model.UserId && x.FormId == model.FormId, true, y => y.Values)
                                              .OrderByDescending(x => x.DateCreated)
                                              .Skip(model.PageNumber * model.PageSize)
                                              .Take(model.PageSize).ToList();

                    return Results.Ok(value: new ResponseModel<List<ReportModel>>(mapper.Map<List<ReportModel>>(reports)));
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

            endpoints.MapPost(ReportEndpoint.CreateReport, [Authorize(AuthenticationSchemes = "Bearer")] async (ReportModel model, IReportService reportService) =>
            {
                try
                {
                    await reportService.CreateReport(model);
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

            endpoints.MapPost(ReportEndpoint.UpdateReport, [Authorize(AuthenticationSchemes = "Bearer")] async (UpdateReportModel model, IReportService reportService) =>
            {
                try
                {
                    await reportService.UpdateReport(model);
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

            endpoints.MapPost(ReportEndpoint.DeleteReport, [Authorize(AuthenticationSchemes = "Bearer")] async (DeleteModel request, IReportService reportService) =>
            {
                try
                {
                    await reportService.DeleteReport(request.Id);
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

            endpoints.MapGet(ReportEndpoint.GetKillReport, [Authorize(AuthenticationSchemes = "Bearer")] async (string userId, IReportTicketRepository reportTicketRepository) =>
            {
                try
                {
                    var data = reportTicketRepository.GetAnimalKillingReport(userId);
                    return Results.Ok(value: new ResponseModel<ICollection<AnimalKillingReportModel>>(data: data));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ListAbttoirReportModel>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ListAbttoirReportModel>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapGet(ReportEndpoint.ListQuarantineReport, [Authorize(AuthenticationSchemes = "Bearer")] async (string userId, IReportTicketRepository reportTicketRepository) =>
            {
                try
                {
                    var data = reportTicketRepository.GetListQuarantineReport(userId);
                    return Results.Ok(value: new ResponseModel<ICollection<ListQuarantineReportModel>>(data: data));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ListQuarantineReportModel>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ListQuarantineReportModel>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapGet(ReportEndpoint.ListAbattoirReport, [Authorize(AuthenticationSchemes = "Bearer")] async (string userId, IReportTicketRepository reportTicketRepository) =>
            {
                try
                {
                    var data = reportTicketRepository.GetListAbattoirReport(userId);
                    return Results.Ok(value: new ResponseModel<ICollection<ListAbttoirReportModel>>(data: data));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ListAbttoirReportModel>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ListAbttoirReportModel>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapGet(ReportEndpoint.RevenueQuarantineReport, [Authorize(AuthenticationSchemes = "Bearer")] async (DateTimeOffset fromDay, DateTimeOffset toDay, IReportTicketRepository reportTicketRepository) =>
            {
                try
                {
                    var data = reportTicketRepository.GetQuarantineRevenueReport(fromDay, toDay);
                    return Results.Ok(value: new ResponseModel<ICollection<QuarantineRevenueReport>>(data: data));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<QuarantineRevenueReport>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<QuarantineRevenueReport>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(ReportEndpoint.BasePath);

            endpoints.MapGet(ReportEndpoint.GetSingleReport, [Authorize(AuthenticationSchemes = "Bearer")] async ( string reportId, IReportTicketRepository reportRepository, IMapper mapper) =>
            {
                try
                {
                    var reports = reportRepository.Get(x => x.Id == reportId)
                                                  .Include(y => y.Values)
                                                  .Include(y => y.SealTabs)
                                                  .Include(y => y.ListAnimals).FirstOrDefault();

                    return Results.Ok(value: new ResponseModel<ReportModel>(mapper.Map<ReportModel>(reports)));
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
