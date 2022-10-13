using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tech.Test.Payment.Domain.Validations;

namespace Tech.Test.Payment.Domain.Entities
{
    public sealed class Sale
    {
        //Propriedades

        //Sales
        [Key]
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public StatusEnum Status { get; set; }
        //Items
        public int IdOrder { get; private set; }
        public string Items { get; private set; }
        public int QuantityItems { get; private set; }
        //Seller
        public int IdSeller { get; private set; }
        public string NameSeller { get; private set; }
        public string Cpf { get; private set; }
        public string Phone { get; private set; }



        //Construtor
        public Sale(string items, int quantityItems, int idOrder, int idSeller, string nameSeller, string cpf, string phone)
        {
            try
            {
                Validation(items, quantityItems, idOrder, idSeller, nameSeller, cpf, phone);
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            
        }

        public void SetId(int id)
        {
            Id = id;
        }


        //Validação


        private void Validation(string items, int quantityItems, int idOrder, int idSeller, string nameSeller, string cpf, string phone)
        {

            DomainValidationException.When(string.IsNullOrEmpty(items), "A Descrição dos items deve ser informada");
            DomainValidationException.When(quantityItems < 1, "A venda precisa de no mínimo 1 item para ser cadastrada");
            DomainValidationException.When(idOrder < 1, "Id do Pedido Inválido");
            DomainValidationException.When(idSeller < 1, "Id do Vendedor Inválido");
            DomainValidationException.When(string.IsNullOrEmpty(nameSeller), "O Nome do vendedor deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(cpf), "O Cpf deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(phone), "O Telefone deve ser informado");

            Date = DateTime.Now;
            Status = StatusEnum.AWAITING_PAYMENT;

            Items = items;
            QuantityItems = quantityItems;
            IdSeller = idSeller;
            IdOrder = idOrder;
            NameSeller = nameSeller;
            Cpf = cpf;
            Phone = phone;
        }
    }
}