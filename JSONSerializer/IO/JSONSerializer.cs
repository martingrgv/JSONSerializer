using System.Reflection;
using System.Text;

namespace MyJSONSerializer.IO
{
    public class JSONSerializer
    {
        private static JSONSerializer _instance;

        protected JSONSerializer()
        {

        }

        public static JSONSerializer Instance()
        {
            if (_instance == null)
            {
                _instance = new JSONSerializer();
            }

            return _instance;
        }

        // TODO: Make deep copy
        private bool IsNumber(string str)
        {
            char[] charArr = str.ToCharArray();

            foreach (char ch in charArr)
            {
                if (char.IsDigit(ch))
                {
                    return true;
                }
            }

            return false;
        }

        public string Serialize(object obj)
        {
            StringBuilder sb = new StringBuilder();

            Type type = obj.GetType();
            var properties = type.GetProperties();


            sb.Append("{");

            foreach (var property in properties)
            {
                sb.Append($"\"{property.Name}\": ");

                var value = property.GetValue(obj);

                if (value == null)
                {
                    sb.Append("null");
                }
                else if (IsNumber(value.ToString()))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append($"\"{value}\"");
                }

                // Implement counter and if counter is < len append
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 5, 5);
            sb.AppendLine("}");

            return sb.ToString().TrimEnd();
        }

        public string Serialize(IEnumerable<object> objects)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (object obj in objects)
            {
                sb.Append(Serialize(obj));
                sb.AppendLine(",");
            }

            sb.Remove(sb.Length - 3, 3);

            sb.AppendLine("]");

            return sb.ToString().TrimEnd();
        }
        
        // TODO: Deserialize with constructor params
        private Dictionary<string, object> GetParametersFromJSON(string jsonString)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            StringBuilder formattedJson = new StringBuilder(jsonString);
            string[] forbiddenChars = { "[", "]", "{", "}" };


            foreach (var str in forbiddenChars)
            {
                formattedJson.Replace(str, "");
            }

            Console.WriteLine(formattedJson.ToString());

            string[] properties = formattedJson.ToString().Split(", ");

            foreach (var property in properties)
            {
                var propKeyName = property.Split(": ")[0];
                var propValueName = property.Split(": ")[1];

                parameters.Add(propKeyName.Replace("\"", ""), propValueName.Replace("\"", ""));
            }

            return parameters;
        }

        public T Deserialize<T>(string jsonString)
        {
            Type type = typeof(T);
            T instance = (T)Activator.CreateInstance(type);

            Dictionary<string, object> propParams = GetParametersFromJSON(jsonString);

            foreach (var pair in propParams)
            {
                PropertyInfo currentProp = instance.GetType().GetProperty(pair.Key);
                object value = GetTypeOfValue(currentProp, pair.Value);

                currentProp.SetValue(instance, value, null);
            }

            return instance;
        }

        private object GetTypeOfValue(PropertyInfo prop, object value)
        {
            string valueStr = value.ToString();

            if (prop.PropertyType == typeof(int))
            {
                return int.Parse(valueStr);
            }
            else if (prop.PropertyType == typeof(float))
            {
                return float.Parse(valueStr);
            }
            else if (prop.PropertyType == typeof(double))
            {
                return double.Parse(valueStr);
            }
            else if (prop.PropertyType == typeof(decimal))
            {
                return decimal.Parse(valueStr);
            }

            return value;
        }
    }
}
