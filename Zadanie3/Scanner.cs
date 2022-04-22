
namespace Zadanie3
{
    public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; private set; } = 0;

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                ScanCounter++;
                string scanFileName = "";
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        scanFileName = $"PDFScan{ScanCounter}.pdf";
                        document = new PDFDocument(scanFileName);
                        break;
                    case IDocument.FormatType.JPG:
                        scanFileName = $"ImageScan{ScanCounter}.jpg";
                        document = new ImageDocument(scanFileName);
                        break;
                    case IDocument.FormatType.TXT:
                        scanFileName = $"TextScan{ScanCounter}.txt";
                        document = new TextDocument(scanFileName);
                        break;
                    default:
                        throw new ArgumentException();
                }

                Console.WriteLine($"{DateTime.Now} Scan: {scanFileName}");
            }
            else
            {
                document = null;
            }
        }
        public void Scan(out IDocument document)
        {
            Scan(out document, IDocument.FormatType.PDF);
        }
    }
}
