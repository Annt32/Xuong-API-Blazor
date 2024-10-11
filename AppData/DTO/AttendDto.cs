﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DTO
{
    public class AttendDto
    {
        public Guid IdAttendance { get; set; }
        public Guid UserId { get; set; }

        public Guid IdShift { get; set; }

        public string Notes { get; set; }
        public int Status { get; set; }
        public decimal AdditionalFee { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BankTransferAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}