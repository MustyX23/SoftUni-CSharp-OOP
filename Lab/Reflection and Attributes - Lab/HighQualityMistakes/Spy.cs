using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string nameToInvestigate, params string[] requestedFields)
        {
            Type classType = Type.GetType(nameToInvestigate);

            FieldInfo[] fields = classType.GetFields
                (BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            StringBuilder result = new StringBuilder();

            result.AppendLine($"Class under investigation: {classType.Name}");

            foreach (FieldInfo field in fields.Where(f => requestedFields.Contains(f.Name)))
            {
                result.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return result.ToString().Trim();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields
                (BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            MethodInfo[] nonPublicMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder result = new StringBuilder();

            foreach (FieldInfo field in fields)
            {
                result.AppendLine($"{field.Name} must be private!");
            }

            foreach (MethodInfo nonPublicMethod in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                result.AppendLine($"{nonPublicMethod.Name} have to be public!");
            }

            foreach (MethodInfo publicMethod in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                result.AppendLine($"{publicMethod.Name} have to be private!");
            }

            return result.ToString().Trim();
        }
    }
}
