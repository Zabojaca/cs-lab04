using System;

namespace Zadanie4
{
    public interface IDevice
    {
        enum State { on, standby, off };

        void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            if (GetState() != State.on)
            {
                SetState(State.on);
                Console.WriteLine("Device is on...");
            }
        }

        void PowerOff() // wyłącza urządzenie, zmienia stan na `off
        {
            if (GetState() != State.off)
            {
                SetState(State.off);
                Console.WriteLine("Device is off...");
            }
        }

        void StandbyOn()
        {
            if (GetState() != State.standby)
            {
                SetState(State.standby);
                Console.WriteLine("Device is standby...");
            }            
        }
        void StandbyOff()
        {
            if (GetState() == State.standby)
            {
                SetState(State.on);
                Console.WriteLine("Device is no longer standby...");
            }
        }

        State GetState(); // zwraca aktualny stan urządzenia
        abstract protected void SetState(State state);
        int Counter { get; } // zwraca liczbę charakteryzującą eksploatację urządzenia,
                             // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}