using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Application.DTO.Validations
{
    public class SaleDTOValidator : AbstractValidator<SaleDTO>
    {
        public SaleDTOValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data deve ser informada");

             RuleFor(x => x.IdOrder)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("O Id do Pedido deve ser informado");

            RuleFor(x => x.Items)
                .NotEmpty()
                .NotNull()
                .WithMessage("A Descrição dos items deve ser informada");

            RuleFor(x => x.QuantityItems)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data deve ser informada")

                .GreaterThan(0)
                .WithMessage("Deve conter ao menos um item na venda");

             RuleFor(x => x.IdSeller)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("O Id do Vendedor deve ser informado");
            
            RuleFor(x => x.NameSeller)
                .NotEmpty()
                .NotNull()
                .WithMessage("O Nome do vendedor deve ser informado");

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .WithMessage("O Cpf deve ser informado");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("O Telefone deve ser informado");

            RuleFor(x => x.Status)
                .Equal(StatusEnum.AWAITING_PAYMENT)
                .WithMessage("Status inválido");
        }
    }
}