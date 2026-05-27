using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF
{
    public static class SessionStateMessenger
    {
        public static event Action<SessionState> SessionStateChanged; 
        public static void SendSessionStateChanged(SessionState state)
        {
            SessionStateChanged?.Invoke(state);
        }
    }
}
