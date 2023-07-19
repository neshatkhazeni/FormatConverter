using FormatConverter.Interface;
using FormatConverter.Models;
using System;

namespace FormatConverter
{
    public class ConvertProcess
    {
        public static void Process(IStorage inputStorage, IFormatConverter<Document> inputConverter, IStorage outputStorage, IFormatConverter<Document> outputConverter)
        {
            try
            {
                //Read
                var input = inputStorage.Read();

                //Convert
                var document = inputConverter.ToObject(input);
                var result = outputConverter.FromObject(document);

                //Write
                outputStorage.Write(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
