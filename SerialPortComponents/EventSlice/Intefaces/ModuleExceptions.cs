using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    /// <summary>
    /// Exceptions generated in modules should be thrown from this class.
    /// </summary>
    public class ModuleException : Exception
    {
        /// <summary>
        /// Human readable explanation of the exception condition.
        /// </summary>
        public string exceptionText { get; private set; }

        /// <summary>
        /// A reference to the module generating the exception.
        /// </summary>
        public Module module { get; private set; }
        /// <summary>
        /// If the exception originated in libraries outside the module, then it can be optionally caught, 
        /// added to a module exception and then thrown.
        /// </summary>
        public Exception originatingExecption { get; private set; }

        /// <summary>
        /// Constructor for exception with an embedded originating exception.
        /// </summary>
        /// <param name="module">A reference to the module generating the exception.</param>
        /// <param name="exceptionText">Human readable explanation of the exception condition.</param>
        /// <param name="originatingException">Embedded exception</param>
        public ModuleException(Module module, string exceptionText, Exception originatingException)
        {
            this.exceptionText = exceptionText;
            this.module = module;
            this.originatingExecption = originatingExecption;
        }

        /// <summary>
        /// Constructor for exception without an embedded exception.
        /// </summary>
        /// <param name="module">A reference to the module generating the exception.</param>
        /// <param name="exceptionText">Human readable explanation of the exception condition.</param>
        public ModuleException(Module module, string exceptionText)
        {
            this.exceptionText = exceptionText;
            this.module = module;
            this.originatingExecption = null;
        }

    }
}
