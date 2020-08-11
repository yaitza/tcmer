using System;
using System.Windows.Media;

namespace TCMER.Utils
{
    class DisplayHelper
    {
        public delegate void ShowMessageHandler(string msg, Color color);

        public static ShowMessageHandler ShowMethod { get; set; }

        public static void ShowMessage(string msg, Color color)
        {
            if (ShowMethod != null)
            {
                ShowMethod.Invoke(msg, color);
            }
        }

        internal static void ShowMessage(string message, object red)
        {
            throw new NotImplementedException();
        }
    }
}
