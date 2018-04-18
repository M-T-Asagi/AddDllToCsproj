using System;
using System.Collections.Generic;

namespace AddUnityDll
{
    class MainProcess
    {
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                throw new Exception("Error! invalid options.");
            }

            string csproj = args[0];
            string config = args[1];

            Root deserializedObject = CsprojSerializer.Deserialize<Root>(csproj);
            Dictionary<string, string> dlls = ConfigReader.Read(config);

            List<Reference> references;
            if (deserializedObject.ItemGroup == null)
            {
                ItemGroup itemGroup = new ItemGroup();
                references = new List<Reference>();
                itemGroup.References = references;
                deserializedObject.ItemGroup = itemGroup;
            } else
            {
                references = deserializedObject.ItemGroup.References;
            }

            foreach(KeyValuePair<string, string> dll in dlls)
            {
                Reference reference = new Reference();
                reference.Include = dll.Key;
                reference.HintPath = dll.Value;

                bool updated = false;

                for(int i = 0;i < references.Count; i++)
                {
                    Reference _ref = references[i];
                    if(_ref.Include == reference.Include || _ref.HintPath == reference.HintPath)
                    {
                        references[i] = reference;
                        updated = true;
                    }
                }

                if (!updated)
                    references.Add(reference);
            }
            
            CsprojSerializer.Serialize(csproj, deserializedObject);
        }
    }
}
