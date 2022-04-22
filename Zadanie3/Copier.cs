
namespace Zadanie3
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public Scanner Scanner { get; private set; } = new Scanner();
        public Printer Printer { get; private set; } = new Printer();

        public void Print(in IDocument document)
        {
            Printer.PowerOn();
            Printer.Print(in document);
            Printer.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            Scanner.PowerOn();
            Scanner.Scan(out document, formatType);
            Scanner.PowerOff();
        }
        public void Scan(out IDocument document)
        {
            Scanner.PowerOn();
            Scanner.Scan(out document, IDocument.FormatType.PDF);
            Scanner.PowerOff();
        }

        public void ScanAndPrint()
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
}
