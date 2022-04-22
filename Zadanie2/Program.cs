using Zadanie1;

namespace Zadanie2
{
    class Program
    {
        public static void Main()
        {
            MultifunctionalDevice device1 = new MultifunctionalDevice();
            MultifunctionalDevice device2 = new MultifunctionalDevice();
            device1.PowerOn();
            device2.PowerOn();
            IDocument doc1;
            device1.Scan(out doc1);
            device1.Send(doc1, device2);
        }        
    }
}
