using System;
using System.IO;
using System.Reflection;

namespace tests.utils
{
    public class TestResources
    {
        public static string Get(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            return result;
        }
    }
}