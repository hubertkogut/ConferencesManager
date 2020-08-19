using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConferencesProject.UITests
{
    internal static class Helper
    {
        public static void Pause(int miliSecondsToPause = 6000)
        {
            Thread.Sleep(miliSecondsToPause);
        }
    }
}
