﻿using System;

namespace LogitWebApp.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public string  UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string DriverId { get; set; }

        public ApplicationUser Driver { get; set; }

        public Byte Value { get; set; }
    }
}
