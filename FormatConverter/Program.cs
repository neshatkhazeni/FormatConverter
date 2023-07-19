using FormatConverter.Implementations.FormatConverters;
using FormatConverter.Implementations.Storages;
using FormatConverter.Interface;
using FormatConverter.Models;
using System;
using System.IO;

namespace FormatConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //FTP Storage Definition
            //IStorage inputStorage = new FtpStorage(Settings.FtpSettings("SourceFiles/DocumentTest.json"));
            //IStorage outputStorage=new FtpStorage(Settings.FtpSettings("TargetFiles/DocumentTest.xml"));

            //File System Storage Definition
            IStorage inputStorage = new FileSystemStorage(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\DocumentTest.json"));
            IStorage outputStorage = new FileSystemStorage(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\DocumentTest.xml"));

            IFormatConverter<Document> inputConverter = new JsonFormatConverter<Document>();
            IFormatConverter<Document> outputConverter = new XmlFormatConverter<Document>();

            //Convert Process
            ConvertProcess.Process(inputStorage, inputConverter, outputStorage, outputConverter);

        }
    }
}
    

