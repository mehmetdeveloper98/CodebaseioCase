using Customer.Application.Dtos;
using Customer.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Customer.Application.Interfaces;
using Shared.CommonModel;

namespace Customer.Application.CQRS.Queries
{
    public class LoginQuery:IRequest<ResponseModel<LoginCommandResponse>>
    {
        public LoginDto loginModel;

        public LoginQuery(LoginDto loginModel)
        {
            this.loginModel = loginModel;
        }
        public class LoginQueryHandler : IRequestHandler<LoginQuery, ResponseModel<LoginCommandResponse>>
        {
            private readonly UserManager<Domain.Entities.Customer> _userManager;
            private readonly IJwtProvider _jwtProvider;

            public LoginQueryHandler(UserManager<Domain.Entities.Customer> userManager, IJwtProvider jwtProvider)
            {
                _userManager = userManager;
                _jwtProvider = jwtProvider;
            }

            public async Task<ResponseModel<LoginCommandResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
           {
                var userExists = await _userManager.FindByEmailAsync(request.loginModel.Email);
                if (userExists == null)
                    throw new NotFoundException("User not found!");

                var result = await _userManager.CheckPasswordAsync(userExists, request.loginModel.Password);
                if (result)
                {
                    var response = await _jwtProvider.CreateTokenAsync(userExists);
                    return ResponseModel<LoginCommandResponse>.Success(200, new LoginCommandResponse {Token= response });
                }
                return ResponseModel<LoginCommandResponse>.Fail(400, "Wrong email or password!");
            }
        }
    }

}
