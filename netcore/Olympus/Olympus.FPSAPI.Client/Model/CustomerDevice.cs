using System;

namespace Olympus.FPSAPI.Client.Model
{
    public class CustomerDevice
    {
        public string Account { get; set; }

        public string Device { get; set; }

        public DateTime LastAccessedDate { get; set; }
    }
}
