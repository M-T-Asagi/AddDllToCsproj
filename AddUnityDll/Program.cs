using System;
using System.Collections.Generic;

namespace AddUnityDll
{
    class MainProcess
    {
        static void Main(string[] args)
        {
            string csproj = args[0];
            string config = args[1];

            Root deserializedObject = CsprojSerializer.Deserialize<Root>(csproj);
            Dictionary<string, string> dlls = ConfigReader.Read(config);



        }
    }
}
