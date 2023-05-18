﻿using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.dtos.CashAdvanceDTOs
{
    public class CashAdvanceUpdateDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal RequestedAmount { get; set; }
        public Guid AdvanceToId { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
