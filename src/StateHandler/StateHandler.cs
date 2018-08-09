using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxaforSharp;

namespace StateHandler
{
    /// <summary>
    /// Manages the base state of the LEDs.  Events may trigger something but the flag will return to the base state.
    ///
    /// When user is AFK (locked), calendarState is shown on the rear LEDs.
    /// When user is present (machine unlocked), focusState is shown on the flag LEDs.
    /// </summary>
    public class StateHandler : IStateHandler
    {
        const int TransitionTime = 7;

        FocusState focusState;
        PhysicalPresence physicalPresence;
        readonly IDevice device;

        public StateHandler(IDevice device, FocusState focusState, PhysicalPresence physicalPresence)
        {
            this.focusState = focusState;
            this.physicalPresence = physicalPresence;
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.UpdateDeviceLEDs();
        }

        public bool TriggerEvent(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Standup:
                    this.device.CarryOutPattern(PatternType.RainbowWave, 1).Wait();
                    break;
            }
            return true;
        }

        public bool SetFocusLevel(FocusState focusState)
        {
            this.focusState = focusState;
            return this.UpdateDeviceLEDs();
        }

        public bool SetPhysicalPresence(PhysicalPresence physicalPresence)
        {
            this.physicalPresence = physicalPresence;
            return this.UpdateDeviceLEDs();
        }

        private bool UpdateDeviceLEDs()
        {
            Color flagColor = ComputeFlagColor(this.physicalPresence, this.focusState);
            Color rearColor = ComputerRearColor(this.physicalPresence);

            return this.device.SetColor(LedTarget.AllFrontSide, flagColor, TransitionTime).Result
                && this.device.SetColor(LedTarget.AllBackSide, rearColor, TransitionTime).Result;
        }

        private void TurnOffDevice()
        {
            this.device.SetColor(LedTarget.AllFrontSide, Colors.Off, TransitionTime).Wait();
            this.device.SetColor(LedTarget.AllBackSide, Colors.Off, TransitionTime).Wait();
        }

        public static Color ComputerRearColor(PhysicalPresence physicalPresence)
        {
            switch (physicalPresence)
            {
                case PhysicalPresence.Present:
                    return Colors.Off;

                default:
                    return Colors.White;
            }
        }

        public static Color ComputeFlagColor(PhysicalPresence physicalPresence, FocusState focusState)
        {
            if (physicalPresence == PhysicalPresence.AFK)
            {
                return Colors.Off;
            }

            switch (focusState)
            {
                case FocusState.High:
                    return Colors.Red;

                case FocusState.Low:
                default:
                    return Colors.Green;
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
                    this.TurnOffDevice();
                    this.device.Dispose();
                }

                this.disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
