using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJSONSerializer.Interfaces
{
    public interface IStreamer
    {
        string Read();
        void Write(string data);
    }
}
