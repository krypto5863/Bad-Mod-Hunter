using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Bad_Mod_Hunter
{
    class LangManager
    {

        public static void ChangeCulture(string sLangCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(sLangCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLangCode);
        }
    }
}