namespace JSONSerializer
{
    public static class StreamService
    {
        private const string FILENAME = "data.json";

        public static string Read()
        {
            using (StreamReader reader = new StreamReader(FILENAME))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Write(string data)
        {
            using (StreamWriter writer = new StreamWriter(FILENAME))
            {
                writer.Write(data);
            }
        }
    }
}
