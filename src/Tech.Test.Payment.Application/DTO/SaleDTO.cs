using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Application.DTO
{
    public class SaleDTO
    {

        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum Status { get; set; }
        //Items
        public int IdOrder { get; set; }
        public string Items { get; set; }
        public int QuantityItems { get; set; }
        //Seller
        public int IdSeller { get; set; }
        public string NameSeller { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
    }
}