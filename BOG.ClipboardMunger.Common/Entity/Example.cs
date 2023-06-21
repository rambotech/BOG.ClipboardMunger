using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.ClipboardMunger.Common.Entity
{
    public class Example
    {
        public string Name { get; set; }
        public Dictionary<string, string> ArgumentValues { get; set; } = new Dictionary<string, string>();
        public string Input { get; set; }
    }
}
