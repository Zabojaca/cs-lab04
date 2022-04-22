using System;
using Zadanie1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public interface IFax : IDevice
    {
        void Send(IDocument document, IFax reciever);
        void Recieve(IDocument document);
    }
}
