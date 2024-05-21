using System.Globalization;
using System.Reflection;
using System.Text;

namespace MyJSONSerializer.IO
{
    public class JSONSerializer
    {
        private static object _locker = new object();
        private static JSONSerializer _instance;

        protected JSONSerializer()
        {

        }

        public static JSONSerializer Instance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new JSONSerializer();
                    }
                }
            }

            return _instance;
        }

        // TODO: Make deep copy
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                throw new InvalidOperationException("Cannot serialize null object!");
            }

            return GetJSONString(obj);
        }

        private string GetJSONString(object obj)
        {
            StringBuilder sb = new StringBuilder();

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            sb.Append("{");

            int i = 0;
            foreach (var property in properties)
            {
                sb.Append($"\"{property.Name}\":");

                object value = property.GetValue(obj);

                if (value == null)
                {
                    sb.Append("null");
                }
                else if (value is int)
                {
                    sb.Append(value);
                }
                else if (value is float || value is double || value is decimal)
                {
                    decimal decimalValue = (decimal)value;
                    sb.Append(decimalValue.ToString(CultureInfo.CreateSpecificCulture("en-GB")));
                }
                else
                {
                    sb.Append($"\"{value}\"");
                }

                if (i < properties.Length - 1)
                {
                    sb.Append(",");
                    i++;
                }
            }

            sb.AppendLine("}");

            return sb.ToString().TrimEnd();
        }

        public string Serialize(IEnumerable<object> objects)
        {
            if (!objects.Any())
            {
                throw new InvalidOperationException("Cannot serialize null objects!");
            }

            return GetJSONEnumerableString(objects);
        }

        private string GetJSONEnumerableString(IEnumerable<object> objects)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            int i = 0;
            foreach (object obj in objects)
            {
                sb.Append(Serialize(obj));

                if (i < objects.Count() - 1)
                {
                    sb.Append(",");
                    i++;
                }
            }

            sb.AppendLine("]");

            return sb.ToString().TrimEnd();
        }

        // TODO: Deserialize with constructor params
        // TODO: Group Methods/Order Methods
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
