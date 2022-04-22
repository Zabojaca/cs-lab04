using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3;
using System;
using System.IO;

namespace Zadanie3UnitTests
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class UnitTestMultidimensionalDevice
    {
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.Printer.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multifunctionalDevice.Scanner.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultidimensionalDevice reciever = new MultidimensionalDevice();
                multifunctionalDevice.Send(doc1, reciever);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sent"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultidimensionalDevice reciever = new MultidimensionalDevice();
                multifunctionalDevice.Send(doc1, reciever);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sent"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Recieve_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Recieve(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Recieved"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Recieve_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Recieve(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Recieved"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}