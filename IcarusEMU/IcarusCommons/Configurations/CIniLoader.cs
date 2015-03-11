// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Runtime.InteropServices;
using System.Text;

namespace IcarusCommons.Configurations
{
    public static class CIniLoader
    {
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string applicationName, string keyName, string strValue,
            string fileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string applicationName, string keyName, string defaultValue,
            StringBuilder returnString, int nSize, string fileName);

        public static void WriteValue(string sectionName, string keyName, string keyValue)
        {
            WritePrivateProfileString(sectionName, keyName, keyValue, @"./server.ini");
        }

        public static string ReadValue(string sectionName, string keyName)
        {
            var szStr = new StringBuilder(255);
            GetPrivateProfileString(sectionName, keyName, "", szStr, 255, @"./server.ini");
            return szStr.ToString().Trim();
        }
    }
}