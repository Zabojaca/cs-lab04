using System;
using Zadanie1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{    
    public class MultifunctionalDevice : Copier, IFax
    {
        public void Send(in IDocument document, IFax reciever)
        {
            if(state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Sent: {document.GetFileName()}");
                reciever.Recieve(document);
            }            
        }
        public void Recieve(in IDocument document)
        {
            if(state == IDevice.State.off)
            {
                Console.WriteLine($"{DateTime.Now} Recieved: {document.GetFileName()}");
            }
        }
    }
}
