using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public interface IFax : IDevice
    {
        void Send(IDocument document, IFax reciever);
        void Recieve(IDocument document);
    }

    public class Fax : BaseDevice, IFax
    {
        public void Send(IDocument document, IFax reciever)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Sent: {document.GetFileName()}");
                reciever.Recieve(document);
            }
        }

        public void Recieve(IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Recieved: {document.GetFileName()}");
            }
        }
    }
}
