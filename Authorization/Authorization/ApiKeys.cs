using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Authorization
{
    public class ApiKeys
    {
        public int ApiKeyId { get; set; }
        public Guid KeyUid { get; set; }
        public string AllowedApi { get; set; }
    }
}
