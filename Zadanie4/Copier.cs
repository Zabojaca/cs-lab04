using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    public class Copier : IPrinter, IScanner
    {
        private IDevice.State printerState = IDevice.State.off;
        private IDevice.State scannerState = IDevice.State.off;
        public int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public IDevice.State GetState() 
        {
            if(printerState == scannerState && printerState == IDevice.State.off)
            {
                return IDevice.State.off;
            }
            else if(printerState == scannerState && printerState == IDevice.State.standby)
            {
                return IDevice.State.standby;
            }
            return IDevice.State.on;
        }

        public void Print(in IDocument document)
        {
            if(printerState == IDevice.State.off)
            {
                (this as IPrinter).PowerOn();
            }
            if (printerState == IDevice.State.standby)
            {
                (this as IPrinter).StandbyOff();
            }
            if (printerState == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                if (PrintCounter % 2 == 0)
                {
                    (this as IPrinter).StandbyOn();
                }
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (printerState == IDevice.State.off)
            {
                (this as IScanner).PowerOn();
            }
            if (scannerState == IDevice.State.standby)
            {
                (this as IScanner).StandbyOff();
            }
            if (scannerState == IDevice.State.on)
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

                if (ScanCounter % 3 == 0)
                {
                    (this as IScanner).StandbyOn();
                }
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

        void IPrinter.Print(in IDocument document)
        {
            throw new NotImplementedException();
        }

        void IDevice.SetState(IDevice.State state)
        {
            printerState = state;
            scannerState = state;
        }
    }
}
