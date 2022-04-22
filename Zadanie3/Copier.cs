
namespace Zadanie3
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public Scanner Scanner { get; private set; } = new Scanner();
        public Printer Printer { get; private set; } = new Printer();

        public void Print(in IDocument document)
        {
            Printer.Print(in document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            Scanner.Scan(out document, formatType);
        }
        public void Scan(out IDocument document)
        {
            Scanner.Scan(out document, IDocument.FormatType.PDF);
        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scanner.Scan(out document, IDocument.FormatType.JPG);
            Printer.Print(document);
        }

        public void PowerOn()
        {
            base.PowerOn();
            Scanner.PowerOn();
            Printer.PowerOn();           
        }

        public void PowerOff()
        {
            base.PowerOff();
            Scanner.PowerOff();
            Printer.PowerOff();            
        }
    }
}
