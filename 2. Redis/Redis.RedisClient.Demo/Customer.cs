﻿using System.Collections.Generic;

namespace Redis.Client.Demo
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }

    }
}
