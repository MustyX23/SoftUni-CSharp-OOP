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
    }
}
