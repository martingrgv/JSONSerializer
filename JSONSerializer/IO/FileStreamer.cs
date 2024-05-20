using MyJSONSerializer.Interfaces;

namespace MyJSONSerializer.IO
{
    public class FileStreamer : IStreamer
    {
        private string _filename;

        public FileStreamer(string filename)
        {
            _filename = filename;
        }

        public string Read()
        {
            using (StreamReader reader = new StreamReader(_filename))
            {
                return reader.ReadToEnd();
            }
        }

        public void Write(string data)
        {
            using (StreamWriter writer = new StreamWriter(_filename))
            {
                writer.Write(data);
            }
        }
    }
}
