using System.Reflection;
using System.Text;

namespace JSONSerializer
{
    public static class JSONService
    {
        private static bool IsNumber(string str)
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

        public static string Serialize(object obj)
        {
            StringBuilder sb = new StringBuilder();

            Type type = obj.GetType();
            var properties = type.GetProperties();


            sb.AppendLine("{");

            foreach (var property in properties)
            {
                sb.Append("\t");
                sb.Append($"\"{property.Name.ToLower()}\": ");

                string value = property.GetValue(obj).ToString();

                if (IsNumber(value))
                {
                    sb.Append($"{value}");
                }
                else
                {
                    sb.Append($"\"{value}\"");
                }

                sb.AppendLine(",");
            }

            sb.Remove(sb.Length - 3, 1);
            sb.AppendLine("}");

            return sb.ToString().TrimEnd();
        }

        public static string Serialize(IEnumerable<object> objects)
        {
            StringBuilder sb = new StringBuilder();

            foreach (object obj in objects)
            {
                sb.Append(Serialize(obj));
                sb.AppendLine(",");
            }

            sb.Remove(sb.Length - 3, 1);

            return sb.ToString().TrimEnd();
        }
    }
}
