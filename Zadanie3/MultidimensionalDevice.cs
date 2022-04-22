
namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        public Scanner Scanner { get; private set; } = new Scanner();
        public Printer Printer { get; private set; } = new Printer();
        public Fax Fax { get; private set; } = new Fax();

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Printer.PowerOn();
                Printer.Print(in document);
                Printer.PowerOff();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                Scanner.PowerOn();
                Scanner.Scan(out document, formatType);
                Scanner.PowerOff();
            }
            else
            {
                document = null;
            }
        }

        public void Scan(out IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Scanner.PowerOn();
                Scanner.Scan(out document, IDocument.FormatType.PDF);
                Scanner.PowerOff();
            }
            else
            {
                document = null;
            }
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                Scanner.PowerOn();
                Scanner.Scan(out document, IDocument.FormatType.JPG);
                Scanner.PowerOff();
                Printer.PowerOn();
                Printer.Print(document);
                Printer.PowerOff();
            }
        }

        public void Send(IDocument document, IFax reciever)
        {
            if (state == IDevice.State.on)
            {
                Fax.PowerOn();
                Fax.Send(document, reciever);
                Fax.PowerOff();
            }
        }

        public void Recieve(IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Fax.PowerOn();
                Fax.Recieve(document);
                Fax.PowerOff();
            }
        }
    }
}
