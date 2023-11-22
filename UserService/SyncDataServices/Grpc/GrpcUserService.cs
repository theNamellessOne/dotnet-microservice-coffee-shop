using AutoMapper;
using Grpc.Core;
using UserService.Data.Repositories;

namespace UserService.SyncDataServices.Grpc;

//GrpcUserBase is automatically generated based off user.proto
public class GrpcUserService(IUserRepository userRepository, IMapper mapper) : GrpcUser.GrpcUserBase
{
    //implementation of protobuf method to get all users
    public override Task<UserResponse> GetAllUsers(GetAllRequest request, ServerCallContext context)
    {
        var users = userRepository.GetAllUsers();
        var response = new UserResponse();

        foreach (var user in users) response.User.Add(mapper.Map<GrpcUserModel>(user));

        return Task.FromResult(response);
    }
}