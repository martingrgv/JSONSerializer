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

            foreach (object obj in objects)
            {
                sb.Append(Serialize(obj));
                sb.AppendLine(",");
            }

            sb.Remove(sb.Length - 3, 3);

            return sb.ToString().TrimEnd();
        }

        //public static T Deserialize<T>(string jsonString)
        //{
        //    return ;
        //}
    }
}
