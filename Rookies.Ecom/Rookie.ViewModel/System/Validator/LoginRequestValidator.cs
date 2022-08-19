using FluentValidation;
using Rookie.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.System.Validator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
             RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
             RuleFor(x => x.PassWord).NotEmpty().WithMessage("PassWord is required")
                .MinimumLength(6).WithMessage("PassWord is at least 6 characters");
        }
    }
}
