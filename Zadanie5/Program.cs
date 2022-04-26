namespace Zadanie5
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);
            IDocument doc3;
            xerox.Scan(out doc3);
            IDocument doc4;
            xerox.Scan(out doc4);
            IDocument doc5;
            xerox.Scan(out doc5);

            xerox.ScanAndPrint();
            xerox.ScanAndPrint();
            xerox.ScanAndPrint();
            xerox.ScanAndPrint();
            xerox.ScanAndPrint();

            System.Console.WriteLine(xerox.Printer.Counter);
            System.Console.WriteLine(xerox.Scanner.Counter);
        }
    }
}