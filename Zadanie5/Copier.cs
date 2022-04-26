using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie5
{
    public class Copier : IPrinter, IScanner
    {
        private IDevice.State state = IDevice.State.on;
        public int Counter { get; private set; } = 0;
        public IPrinter Printer { get; private set; } = new Printer();
        public IScanner Scanner { get; private set; } = new Scanner();

        public IDevice.State GetState() {
            if(Scanner.GetState() == Printer.GetState() && Scanner.GetState() == IDevice.State.off)
            {
                return IDevice.State.off;
            }
            if (Scanner.GetState() == Printer.GetState() && Scanner.GetState() == IDevice.State.standby)
            {
                return IDevice.State.standby;
            }
            return IDevice.State.on;
        }

        void IDevice.SetState(IDevice.State state)
        {
            this.state = state;
        }

        public void Print(in IDocument document)
        {
            if (Printer.GetState() == IDevice.State.off)
            {
                Printer.PowerOn();
            }
            Printer.Print(in document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (Scanner.GetState() == IDevice.State.off)
            {
                Scanner.PowerOn();
            }
            Scanner.Scan(out document, formatType);
        }

        public void Scan(out IDocument document)
        {
            if (Scanner.GetState() == IDevice.State.off)
            {
                Scanner.PowerOn();
            }
            Scanner.Scan(out document, IDocument.FormatType.PDF);
        }

        public void ScanAndPrint()
        {
            IDocument document;
            if (Scanner.GetState() == IDevice.State.off)
            {
                Scanner.PowerOn();
            }
            Scanner.Scan(out document, IDocument.FormatType.JPG);

            if (Printer.GetState() == IDevice.State.off)
            {
                Printer.PowerOn();
            }
            Printer.Print(document);
        }
    }
}
