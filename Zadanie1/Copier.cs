using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
        }

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

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
