using Thu_y.Modules.UserModule.Ports;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Utils.Infrastructure.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ReportModule.Model;
using AutoMapper;

namespace Thu_y.Modules.UserModule.Endpoints
{
    public class UserEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/user";
        public const string GetUserById = BasePath;
        public const string CreateUser = BasePath + "/create-user";
        public const string UpdateUser = BasePath + "/update-user";
        public const string DeleteUser = BasePath + "/delete-user";
        public const string Login = BasePath + "/login";
        public const string GetUser = BasePath + "/get-user";
    }

    public static class UserRoute
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(UserEndpoint.CreateUser, async (UserModel model, IUserService userService) =>
            {
                try
                {
                    if (userService.CreateUser(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                    }
                    throw new Exception("Failed to create!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapPost(UserEndpoint.UpdateUser, async (UserModel model, IUserService userService) =>
            {
                try
                {
                    if (userService.UpdateUser(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                    }
                    throw new Exception("Failed to update!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapDelete(UserEndpoint.DeleteUser, async (string id, IUserService userService) =>
            {
                try
                {
                    if (userService.DeleteUser(id))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                    }
                    throw new Exception("Failed to delete!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapPost(UserEndpoint.Login, (UserDtoModel request, IUserService userService) =>
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                    return Results.NotFound(new ResponseModel<ResponseLoginModel>(message: "Not found User"));

                var user = userService.GetByAccount(request.UserName);
                if (user == null)
                    return Results.NotFound(new ResponseModel<ResponseLoginModel>(message: "Not found User"));

                if (user.Password != request.Password)
                    return Results.NotFound(new ResponseModel<ResponseLoginModel>(message: "Wrong password"));
                var token = userService.CreateJWTToken(user);
                var data = new ResponseLoginModel
                {
                    Name = user.Name,
                    Account = user.Account,
                    Role = user.Role,
                    Token = token
                };
                return Results.Ok(new ResponseModel<ResponseLoginModel>(data: data));
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapGet(UserEndpoint.GetUser, [Authorize(AuthenticationSchemes = "Bearer")] async (int pageIndex, int pageNumber,IUserService userService, IMapper mapper) =>
            {
                try
                {
                    var listUser = userService.GetAccount(pageIndex,pageNumber);
                    if (listUser == null) throw new Exception("NoT found form!") { HResult = 400 };

                    return Results.Ok(value: new ResponseModel<ICollection<UserGetListModel>>(mapper.Map<ICollection<UserGetListModel>>(listUser)));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            return endpoints;
        }
    }
}
