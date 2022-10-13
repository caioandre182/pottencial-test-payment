using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Application.DTO.Validations;
using Tech.Test.Payment.Application.Services.Interfaces;
using Tech.Test.Payment.Domain.Entities;
using Tech.Test.Payment.Domain.Repositories;


namespace Tech.Test.Payment.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        //Create
        public async Task<ResultService<SaleDTO>> CreateAsync(SaleDTO saleDTO)
        {
            var checkSale = await _saleRepository.GetByIdAsync(saleDTO.Id);

            if(checkSale != null)
                return ResultService.Fail<SaleDTO>("Venda já cadastrada no sistema");

            if(saleDTO == null)
                return ResultService.Fail<SaleDTO>("Objeto deve ser informado");

            var result = new SaleDTOValidator().Validate(saleDTO);
            if(!result.IsValid)
                return ResultService.RequestError<SaleDTO>("Problemas de Validação", result);

            var sale = _mapper.Map<Sale>(saleDTO);
            var data = await _saleRepository.CreateAsync(sale);
            return ResultService.Ok<SaleDTO>(_mapper.Map<SaleDTO>(data));
        }

        //Edit
        public async Task<ResultService> UpdateAsync(SaleDTO saleDTO)
        {
            Sale statusVenda = await _saleRepository.GetStatus(saleDTO.Id);

            StatusEnum status = statusVenda.Status;

            bool validation = ValidateStatus(saleDTO, status);
            if(!validation)
                return ResultService.Fail("Status inválido");

            if(saleDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var sale = await _saleRepository.GetByIdAsync(saleDTO.Id);
            
            if(sale == null)
                return ResultService.Fail("Venda não encontrada");

            sale = _mapper.Map<SaleDTO, Sale>(saleDTO, sale);

            await _saleRepository.UpdateAsync(sale);
            
            return ResultService.Ok("Venda Editada");
        }


        //Get
        public async Task<ResultService<SaleDTO>> GetByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if(sale == null)
                return ResultService.Fail<SaleDTO>("Venda não encontrada");
            return ResultService.Ok(_mapper.Map<SaleDTO>(sale));
        }

        //Valida Status
        public bool ValidateStatus(SaleDTO saleDTO, StatusEnum status)
        {
            //AWAITING_PAYMENT
            if(status == StatusEnum.AWAITING_PAYMENT)
            {
                if(saleDTO.Status == StatusEnum.PAYMENT_ACCEPTED | saleDTO.Status == StatusEnum.CANCELED)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //PAYMENT_ACCEPTED
            else if(status == StatusEnum.PAYMENT_ACCEPTED)
            {
                if(saleDTO.Status == StatusEnum.SENT_FOR_TRANSPORT | saleDTO.Status == StatusEnum.CANCELED)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //SENT_FOR_TRANSPORT
            else if(status == StatusEnum.SENT_FOR_TRANSPORT)
            {
                if(saleDTO.Status == StatusEnum.DELIVERED)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
}