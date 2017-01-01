using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public interface IEngine
    {
        EntryProcessingResult OnEntry(InterceptionEntryModel entryModel);
        void OnException(InterceptionExceptionModel exceptionModel);
        void OnExit(InterceptionExitModel exitModel);
    }
}
