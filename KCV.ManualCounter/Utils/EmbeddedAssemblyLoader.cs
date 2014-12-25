using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Gizeta.KCV.ManualCounter.Utils
{
    public class EmbeddedAssemblyLoader
    {
        private static Dictionary<string, Assembly> dic = null;

        public static void Load(string embeddedResource, string fileName)
        {
            if (dic == null)
                dic = new Dictionary<string, Assembly>();

            byte[] ba = null;
            Assembly asm = null;
            Assembly curAsm = Assembly.GetExecutingAssembly();

            using (Stream stm = curAsm.GetManifestResourceStream(embeddedResource))
            {
                if (stm == null)
                    throw new Exception(embeddedResource + " is not found in Embedded Resources.");

                ba = new byte[(int)stm.Length];
                stm.Read(ba, 0, (int)stm.Length);
                try
                {
                    asm = Assembly.Load(ba);

                    dic.Add(asm.FullName, asm);
                }
                catch { }
            }
        }

        public static Assembly Get(string assemblyFullName)
        {
            if (dic == null || dic.Count == 0)
                return null;

            if (dic.ContainsKey(assemblyFullName))
                return dic[assemblyFullName];

            return null;
        }
    }
}
