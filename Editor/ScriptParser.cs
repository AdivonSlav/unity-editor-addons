using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FITEditorAddons.ScriptParser
{
    public class ScriptParser
    {
        public static void ParseScript(string filePath, string genNamespace)
        {
            List<string> scriptLines = ReadFromScript(filePath);
    
            if (scriptLines == null)
            {
                Debug.Log($"Script already contains namespace: {filePath}");
                return;
            }

            //  Incase the script file is not empty we need to find the correct insertion spot
            //  If it is empty, then we can just add the namespace declaration to the list
            if (scriptLines.Count != 0)
            {
                int insertIndex = scriptLines.FindIndex(str => str.Contains("class") && !str.Contains("//"));
                scriptLines.Insert(insertIndex, $"namespace {genNamespace}");
                scriptLines.Insert(insertIndex + 1, "{");
    
                // insertIndex + 2 in order to not indent namespace and its opening bracket
                for (int i = insertIndex + 2; i < scriptLines.Count; i++)
                    scriptLines[i] = "    " + scriptLines[i];
            
                scriptLines.Add("}");
            }
            else
            {
                scriptLines.Add($"namespace {genNamespace}");
                scriptLines.Add("{");
                scriptLines.Add("");
                scriptLines.Add("}");
            }
            

            WriteToScript(scriptLines.ToArray(), filePath);
            
            Debug.Log($"Edited in a namespace for script: {filePath}");
        }
    
        private static void WriteToScript(string[] scriptLines, string filePath)
        {
            using (var sw = new StreamWriter(filePath, false))
            {
                foreach (var line in scriptLines)
                {
                    sw.WriteLine(line);
                }
            }
        }
    
        private static List<string> ReadFromScript(string filePath)
        {
            List<string> lines = new List<string>();
            
            using (var sr = new StreamReader(filePath))
            {
                string line = "";
                
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("namespace") )
                        return null;
                    
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}

