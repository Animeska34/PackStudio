using System;
using System.Collections.Generic;
using System.IO;

namespace PackStudio.Importers
{
    public static class ImportManager
    {
        public struct Importer
        {
            public readonly string name;
            public readonly Type importer;
            public readonly string[] extensions;
            public bool Niyau(string name)
            {
                return this.name == name;
            }
            public bool Niyau(Type t)
            {
                return importer.FullName == t.FullName;
            }
            public bool Niyau(object o)
            {
                return importer.FullName == o.GetType().FullName;
            }
            public bool Niyau<T>()
            {
                return importer.FullName == typeof(T).FullName;
            }

            public bool CanImport(string extension)
            {
                foreach (var item in extensions)
                {
                    if(item==extension)
                        return true;
                }
                return false;
            }

            public Importers.Importer? Instantiate()
            {
                return Activator.CreateInstance(importer) as Importers.Importer;
            }
            public Importer(string name, Type importer, params string[] extensions)
            {
                if (!importer.IsAssignableFrom(typeof(Importers.Importer)))
                {
                    throw new Exception($"{importer.FullName} is not assignable from importer");
                }

                this.name = name;
                this.importer = importer;
                this.extensions = extensions;
            }
        }

        private static List<Importer> importers = new();

        public static Importers.Importer? GetImporter(string name)
        {
            foreach (var item in importers)
            {
                if (item.Niyau(name))
                    return item.Instantiate();
            }
            return null;
        }
        public static string? GetName<T>()
        {
            foreach (var item in importers)
            {
                if (item.Niyau<T>())
                    return item.name;
            }
            return null;
        }
        public static string? GetName(Type t)
        {
            foreach (var item in importers)
            {
                if (item.Niyau(t))
                    return item.name;
            }
            return null;
        }
        public static Importers.Importer? Import(string path)
        {
            var extension = Path.GetExtension(path);
            foreach (var item in importers)
            {
                if(item.CanImport(extension))
                    return item.Instantiate();
            }
            return null;
        }


        public static void Register<T>(string name, params string[] extensions)
        {
            importers.Add(new(name, typeof(T), extensions));
        }
        public static void Register(string name, Type t, params string[] extensions)
        {
            importers.Add(new(name, t, extensions));
        }

        static ImportManager()
        {
        }
    }
}
