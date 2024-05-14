using System.Text;

namespace JSONSerializer.Services
{
    public static class JSONService
    {
        // TODO: Make deep copy, not shallow as it is
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
                sb.Append("  ");
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

                sb.AppendLine(",");
            }

            sb.Remove(sb.Length - 3, 2);
            sb.AppendLine("}");

            return sb.ToString().TrimEnd();
        }

        public static string Serialize(IEnumerable<object> objects)
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

        //public static T Deserialize<T>(string jsonString)
        //{
        //    return ;
        //}
    }
}
