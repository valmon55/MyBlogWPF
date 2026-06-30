using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Configuration
{
    public class ApiSettings
    {
        public string BaseURL { get; set; } = string.Empty;
        public TimeoutSettings Timeouts { get; set; } = new TimeoutSettings();
    }
}
