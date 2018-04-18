using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AddUnityDll
{
    public static class ConfigReader
    {
        public static Dictionary<string, string> Read(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                Dictionary<string, string> dlls = new Dictionary<string, string>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values.Length < 2)
                            continue;

                        dlls[values[0]] = values[1];
                    }
                }

                return dlls;
            }
        }
    }
}
