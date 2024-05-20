using MyJSONSerializer.Interfaces;

namespace MyJSONSerializer.Services
{
    public class StreamService : IStreamer
    {
        private IStreamer _streamer;

        public StreamService(IStreamer streamer)
        {
            _streamer = streamer;
        }

        public string Read()
        {
            return _streamer.Read();
        }

        public void Write(string data)
        {
            _streamer.Write(data);
        }
    }
}
