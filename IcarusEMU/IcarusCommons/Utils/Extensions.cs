// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace IcarusCommons.Utils
{
    public static class Extensions
    {
        private static readonly DateTime StaticDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Random Randomizer = new Random((int) DateTime.Now.Ticks);

        /// <summary>
        ///     Global random
        /// </summary>
        public static Random Random
        {
            get { return Randomizer; }
        }

        /// <summary>
        ///     Property, that return current rounded int UTC time
        /// </summary>
        public static int RoundedUtc
        {
            get { return (int) (Utc/1000); }
        }

        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length*sizeof (char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        ///     Property, that return current long UTC time
        /// </summary>
        public static long Utc
        {
            get { return (long) (DateTime.UtcNow - StaticDate).TotalMilliseconds; }
        }

        public static LoggingConfiguration NLogDefaultConfiguration
        {
            get
            {
                var config = new LoggingConfiguration();

                var consoleTarget = new ColoredConsoleTarget
                {
                    Layout =
                        "${time} | ${message}${onexception:${newline}EXCEPTION OCCURRED${newline}${exception:format=tostring}}",
                    UseDefaultRowHighlightingRules = false
                };
                config.AddTarget("console", consoleTarget);

                var fileTarget = new FileTarget
                {
                    Layout =
                        "${time} | ${message}${onexception:${newline}EXCEPTION OCCURRED${newline}${exception:format=tostring}}",
                    FileName = "${basedir}/Logs/${shortdate}/${level}.txt"
                };
                config.AddTarget("file", fileTarget);

                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Info",
                    ConsoleOutputColor.Gray,
                    ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Warn",
                    ConsoleOutputColor.Yellow,
                    ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Error",
                    ConsoleOutputColor.Red,
                    ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Fatal",
                    ConsoleOutputColor.Red,
                    ConsoleOutputColor.White));

                var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(rule1);

                var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule2);

                return config;
            }
        }

        private static readonly string[] Baths;

        static Extensions()
        {
            Baths = new string[256];
            for (int i = 0; i < 256; i++)
                Baths[i] = String.Format("{0:X2}", i);
        }

        public static string ToHex(this byte[] array)
        {
            var builder = new StringBuilder(array.Length*2);

            foreach (byte t in array)
                builder.Append(Baths[t]);

            return builder.ToString();
        }

        public static string FormatHex(this byte[] data)
        {
            var builder = new StringBuilder(data.Length*4);

            int count = 0;
            int pass = 1;
            foreach (byte b in data)
            {
                if (count == 0)
                    builder.AppendFormat("{0,-6}\t", "[" + (pass - 1)*16 + "]");

                count++;
                builder.Append(b.ToString("X2"));
                if (count == 4 || count == 8 || count == 12)
                    builder.Append(" ");
                if (count == 16)
                {
                    builder.Append("\t");
                    for (int i = (pass*count) - 16; i < (pass*count); i++)
                    {
                        var c = (char) data[i];
                        if (c > 0x1f && c < 0x80)
                            builder.Append(c);
                        else
                            builder.Append(".");
                    }
                    builder.Append("\r\n");
                    count = 0;
                    pass++;
                }
            }

            return builder.ToString();
        }

        public static byte[] ToBytes(this String hexString)
        {
            try
            {
                var result = new byte[hexString.Length/2];

                for (int index = 0; index < result.Length; index++)
                {
                    string byteValue = hexString.Substring(index*2, 2);
                    result[index] = Byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid hex string: {0}", hexString);
                throw;
            }
        }

        public static int ToInt(this string addr)
        {
            return IPAddress.NetworkToHostOrder((int) IPAddress.Parse(addr).Address);
        }

        public static string ReverseIp(this int ip)
        {
            return BitConverter.GetBytes(ip).Reverse().ToArray().ToHex();
        }
    }
    public static class ScriptsCompiler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static readonly CSharpCodeProvider Provider =
            new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });

        private static readonly CompilerParameters CompilerParams =
            new CompilerParameters { GenerateInMemory = true };


        static ScriptsCompiler()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    string location = assembly.Location;
                    if (!string.IsNullOrEmpty(location))
                    {
                        CompilerParams.ReferencedAssemblies.Add(location);
                    }
                }
                catch (NotSupportedException)
                {
                    //Log.Warn("Assembly '{0}' is dynamic, and not supported by script compiler", assembly.GetName()); 
                }
            }
        }

        /// <summary>
        /// Compile type from file and return new instance by calling
        /// constructor with selected parametres
        /// </summary>
        /// <typeparam name="T">Base type</typeparam>
        /// <param name="filepath">File location</param>
        /// <param name="param">Params</param>
        /// <returns></returns>
        public static T Compile<T>(string filepath, params object[] param)
        {
            CompilerResults results =
                Provider.
                    CompileAssemblyFromSource(CompilerParams, File.ReadAllText(filepath));

            if (results.Errors.Count > 0)
                foreach (CompilerError err in results.Errors)
                    Log.Error("Script compile error\nFile: {2}\nLine: {0}\nError:{1}", err.Line, err, filepath);
            else
                return (T)Activator.CreateInstance(results.CompiledAssembly.GetTypes()[0], param);

            return default(T);
        }
    }
}