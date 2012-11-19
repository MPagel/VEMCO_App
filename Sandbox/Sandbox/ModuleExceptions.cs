using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class ModuleException : Exception
    {
        public string exceptionText { get; private set; }
        public Module module { get; private set; }
        public Exception originatingExecption { get; private set; }

        public ModuleException(Module module, string exceptionText, Exception originatingException)
        {
            this.exceptionText = exceptionText;
            this.module = module;
            this.originatingExecption = originatingExecption;
        }

        public ModuleException(Module module, string exceptionText)
        {
            this.exceptionText = exceptionText;
            this.module = module;
            this.originatingExecption = null;
        }

    }
}
