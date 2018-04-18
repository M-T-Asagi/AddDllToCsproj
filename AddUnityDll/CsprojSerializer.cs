﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AddUnityDll
{
    public static class CsprojSerializer
    {
        public static void Serialize<T>(string savePath, T graph)
        {
            using (var sw = new StreamWriter(savePath, false, Encoding.UTF8))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                new XmlSerializer(typeof(T)).Serialize(sw, graph, ns);
            }
        }

        public static T Deserialize<T>(string loadPath)
        {
            using (var sr = new StreamReader(loadPath))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(sr);
            }
        }
    }

    [XmlRoot(ElementName = "Project")]
    public class Root
    {
        [XmlElement(ElementName = "PropertyGroup")]
        public List<PropertyGroup> PropertyGroups;

        [XmlElement(ElementName = "ItemGroup")]
        public ItemGroup ItemGroup;
    }

    public class PropertyGroup
    {
        [XmlAttribute(AttributeName = "Condition")]
        public string Condition;

        [XmlElement(ElementName = "TargetFramework")]
        public string TargetFramework;

        [XmlElement(ElementName = "PreBuildEvent")]
        public string PreBuildEvent;

        [XmlElement(ElementName = "PlatformTarget")]
        public string PlatformTarget;
    }

    public class ItemGroup
    {
        [XmlElement(ElementName = "Reference")]
        public List<Reference> References;
    }

    public class Reference
    {
        [XmlAttribute(AttributeName = "Include")]
        public string Include;

        [XmlElement(ElementName = "HintPath")]
        public string HintPath;
    }
}