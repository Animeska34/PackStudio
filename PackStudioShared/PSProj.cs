using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Xml;
using System.Xml.XPath;

namespace PackStudio
{
    public static class PSProj
    {
        static XmlDocument doc;
        static XPathNavigator navigator;
        static string path;

        public static void Load(string project)
        {
            doc = new();
            doc.LoadXml(File.ReadAllText(project));
            navigator = doc.CreateNavigator();
        }

        public static List<string> GetLoadedPackages()
        {
            List<string> ret = new();

            if (doc == null)
            {
                Debug.WriteLine("Project not loaded");
                return ret;
            }
            var nodes = navigator.Select("//Packages/");
            foreach (XmlNode node in nodes)
            {

            }
            return ret;
        }
    }
}
