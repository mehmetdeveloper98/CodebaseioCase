using Customer.Application.Dtos;
using Customer.Application.Dtos.Common;
using Customer.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonModel;

namespace Customer.Application.CQRS.Commands
{
    public class RegisterCommand : IRequest<ResponseModel<NoContent>>
    {
        public RegisterDto registerModel;

        public RegisterCommand(RegisterDto registerModel)
        {
            this.registerModel = registerModel;
        }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseModel<NoContent>>
        {
            private readonly UserManager<Domain.Entities.Customer> _userManager;

            public RegisterCommandHandler(UserManager<Domain.Entities.Customer> userManager)
            {
                _userManager = userManager;
            }

            public async Task<ResponseModel<NoContent>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var userExists = await _userManager.FindByNameAsync(request.registerModel.Username);
                if (userExists != null)
                    throw new ServerSideException("User already exists!");

                var response = await _userManager.CreateAsync(new Domain.Entities.Customer()
                {
                    Email = request.registerModel.Email,
                    UserName = request.registerModel.Username,
                    PhoneNumber = request.registerModel.Phone
                }, request.registerModel.Password);

                if (!response.Succeeded)
                {
                    List<string> errors = new();
                    foreach (var item in response.Errors)
                    {
                        errors.Add(item.Description);
                    }
                    return ResponseModel<NoContent>.Fail(400, errors);
                }
                return ResponseModel<NoContent>.Success(204);
            }
        }
    }
}
