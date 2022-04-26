using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie5
{
    public class Printer : IPrinter
    {
        private IDevice.State state = IDevice.State.off;
        public int Counter { get; private set; } = 0;
        public IDevice.State GetState() { return state; }

        void IDevice.SetState(IDevice.State state)
        {
            this.state = state;
        }

        public void Print(in IDocument document)
        {
            if(state == IDevice.State.standby)
            {
                (this as IPrinter).StandbyOff();
            }
            if (state == IDevice.State.on)
            {
                Counter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                if(Counter % 2 == 0)
                {
                    (this as IPrinter).StandbyOn();
                }
            }
        }
    }
}
