using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Configuration
{
    public class TimeoutSettings
    {
        public int RequestTimeoutSeconds { get; set; } = 30;
        public int RetryCount { get; set; } = 3;
    }
}
