using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxaforSharp;
using Microsoft.Win32;
using StateHandler;

namespace SessionConsole
{
    public class WindowsSession : IDisposable
    {
        readonly IStateHandler stateHandler;

        public WindowsSession()
        {
            IDeviceList list = new DeviceList();
            list.Scan();
            IDevice device = list.First() ?? throw new ApplicationException("Could not find a device");
            this.stateHandler = new StateHandler.StateHandler(device, FocusState.Low, PhysicalPresence.Present);

            this.Initialize();
        }

        private void Initialize()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(this.HandleSessionSwitch);
        }

        public void Standup()
        {
            this.stateHandler.TriggerEvent(EventType.Standup);
        }

        public void SetIsFocussed(bool isFocussing)
        {
            this.stateHandler.SetFocusLevel(isFocussing ? FocusState.High : FocusState.Low);
        }

        private void HandleSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    this.stateHandler.SetPhysicalPresence(PhysicalPresence.Present);
                    break;

                case SessionSwitchReason.SessionLogoff:
                case SessionSwitchReason.SessionLock:
                    this.stateHandler.SetPhysicalPresence(PhysicalPresence.AFK);
                    break;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.stateHandler.Dispose();
                    SystemEvents.SessionSwitch -= this.HandleSessionSwitch;
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }
        #endregion
    }
}
