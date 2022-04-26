using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie5
{
    public class Scanner : IScanner
    {
        private IDevice.State state = IDevice.State.off;
        public int Counter { get; private set; } = 0;
        public IDevice.State GetState() { return state; }

        void IDevice.SetState(IDevice.State state)
        {
            this.state = state;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if(state == IDevice.State.standby)
            {
                (this as IScanner).StandbyOff();
            }
            if (state == IDevice.State.on)
            {
                Counter++;
                string scanFileName = "";
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        scanFileName = $"PDFScan{Counter}.pdf";
                        document = new PDFDocument(scanFileName);
                        break;
                    case IDocument.FormatType.JPG:
                        scanFileName = $"ImageScan{Counter}.jpg";
                        document = new ImageDocument(scanFileName);
                        break;
                    case IDocument.FormatType.TXT:
                        scanFileName = $"TextScan{Counter}.txt";
                        document = new TextDocument(scanFileName);
                        break;
                    default:
                        throw new ArgumentException();
                }

                Console.WriteLine($"{DateTime.Now} Scan: {scanFileName}");

                if(Counter % 3 == 0)
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
    }
}
