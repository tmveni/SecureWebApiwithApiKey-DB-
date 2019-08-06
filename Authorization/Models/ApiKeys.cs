using System;
using System.Collections.Generic;

namespace Authorization.Models
{
    public partial class ApiKeys
    {
        public Guid Id { get; set; }
        public string AllowedApi { get; set; }
    }
}
