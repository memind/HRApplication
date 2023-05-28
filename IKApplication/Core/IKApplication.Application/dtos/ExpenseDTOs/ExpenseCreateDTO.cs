﻿using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseCreateDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})$", ErrorMessage = "Please separate cents with commas. You can enter up to 2 digits after the comma.")]
        public string AmountString { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public Guid CompanyId { get; set; }
        public ExpenseType Type { get; set; }
        public Currency Currency { get; set; }
    }
}