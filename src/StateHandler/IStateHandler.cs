using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateHandler
{
    public interface IStateHandler : IDisposable
    {
        bool SetPhysicalPresence(PhysicalPresence physicalPresence);
        bool SetFocusLevel(FocusState focusState);
        bool TriggerEvent(EventType eventType);

    }
}
