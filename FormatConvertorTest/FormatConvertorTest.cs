using FormatConverter;
using FormatConverter.Implementations.FormatConverters;
using FormatConverter.Implementations.Storages;
using FormatConverter.Interface;
using FormatConverter.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FormatConvertorTest
{
    public class FormatConvertorTest
    {
        [Test]
        public void FileSystem_XmlToJson()
        {
            //File System Storage Definition
            var input = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\DocumentTest.xml");
            var output = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\DocumentTest.json");
            IStorage inputStorage = new FileSystemStorage(input);
            IStorage outputStorage = new FileSystemStorage(output);
            IFormatConverter<Document> inputConverter = new XmlFormatConverter<Document>();
            IFormatConverter<Document> outputConverter = new JsonFormatConverter<Document>();

            //Convert Process
            ConvertProcess.Process(inputStorage, inputConverter, outputStorage, outputConverter);

            //Test
            string result = File.ReadAllText(output);
            Document jsonObject = JsonConvert.DeserializeObject<Document>(result);
            Assert.That(jsonObject.Title, Is.EqualTo("SampleTitle"));
            Assert.That(jsonObject.Text, Is.EqualTo("SampleText"));
        }

        [Test]
        public void FileSystem_JsonToXml()
        {
            //File System Storage Definition
            var input = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\DocumentTest.json");
            var output = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\DocumentTest.xml");
            IStorage inputStorage = new FileSystemStorage(input);
            IStorage outputStorage = new FileSystemStorage(output);
            IFormatConverter<Document> inputConverter = new JsonFormatConverter<Document>();
            IFormatConverter<Document> outputConverter = new XmlFormatConverter<Document>();

            //Convert Process
            ConvertProcess.Process(inputStorage, inputConverter, outputStorage, outputConverter);

            //Test
            string result = File.ReadAllText(output);
            var xdoc = XDocument.Parse(result);
            XmlSerializer serializer = new XmlSerializer(typeof(Document), "");
            using (var reader = xdoc.CreateReader())
            {
                var xmlObject = (Document)serializer.Deserialize(reader);
                Assert.That(xmlObject.Title, Is.EqualTo("SampleTitle"));
                Assert.That(xmlObject.Text, Is.EqualTo("SampleText"));
            }
        }

        [Test]
        public void Ftp_XmlToJson()
        {
            //Ftp Storage Definition
            var input = Settings.FtpSettings("SourceFiles/DocumentTest.xml");
            var output = Settings.FtpSettings("TargetFiles/DocumentTest.json");
            IStorage inputStorage = new FtpStorage(input);
            IStorage outputStorage = new FtpStorage(output);
            IFormatConverter<Document> inputConverter = new XmlFormatConverter<Document>();
            IFormatConverter<Document> outputConverter = new JsonFormatConverter<Document>();

            //Convert Process
            ConvertProcess.Process(inputStorage, inputConverter, outputStorage, outputConverter);

            //Test
            using (var request = new WebClient())
            {
                request.Credentials = new NetworkCredential(output.Username, output.Password);
                try
                {
                    byte[] file = request.DownloadData(new Uri(output.Server + output.Path));
                    string result = System.Text.Encoding.UTF8.GetString(file);

                    Document jsonObject = JsonConvert.DeserializeObject<Document>(result);
                    Assert.That(jsonObject.Title, Is.EqualTo("SampleTitle"));
                    Assert.That(jsonObject.Text, Is.EqualTo("SampleText"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        [Test]
        public void Ftp_JsonToXml()
        {
            //Ftp Storage Definition
            var input = Settings.FtpSettings("SourceFiles/DocumentTest.json");
            var output = Settings.FtpSettings("TargetFiles/DocumentTest.xml");
            IStorage inputStorage = new FtpStorage(input);
            IStorage outputStorage=new FtpStorage(output);
            IFormatConverter<Document> inputConverter = new JsonFormatConverter<Document>();
            IFormatConverter<Document> outputConverter = new XmlFormatConverter<Document>();

            //Convert Process
            ConvertProcess.Process(inputStorage, inputConverter, outputStorage, outputConverter);

            //Test
            using (var request = new WebClient())
            {
                request.Credentials = new NetworkCredential(output.Username, output.Password);
                try
                {
                    byte[] file = request.DownloadData(new Uri(output.Server + output.Path));
                    string result = System.Text.Encoding.UTF8.GetString(file);

                    var xdoc = XDocument.Parse(result);
                    XmlSerializer serializer = new XmlSerializer(typeof(Document), "");
                    using (var reader = xdoc.CreateReader())
                    {
                        var xmlObject = (Document)serializer.Deserialize(reader);
                        Assert.That(xmlObject.Title, Is.EqualTo("SampleTitle"));
                        Assert.That(xmlObject.Text, Is.EqualTo("SampleText"));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }



    }
}