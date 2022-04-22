namespace Zadanie3
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);
            IDocument doc3;
            xerox.Scan(out doc3);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.Printer.PrintCounter);
            System.Console.WriteLine(xerox.Scanner.ScanCounter);

            var multi1 = new MultidimensionalDevice();
            var multi2 = new MultidimensionalDevice();
            multi1.PowerOn();
            multi2.PowerOn();
            multi1.ScanAndPrint();
            IDocument doc4;
            multi1.Scan(out doc4);
            multi1.Send(doc4, multi2);
            multi1.PowerOff();
            multi2.PowerOff();

            IDocument doc5;
            multi1.PowerOn();
            multi1.Scan(out doc1, formatType: IDocument.FormatType.JPG);
            multi1.Scan(out doc1, formatType: IDocument.FormatType.TXT);
            multi1.Scan(out doc1, formatType: IDocument.FormatType.PDF);
        }
    }
}