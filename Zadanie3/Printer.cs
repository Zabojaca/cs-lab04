
namespace Zadanie3
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
        }
    }
}
