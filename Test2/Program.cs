using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "path_to_your_file.txt"; 
            int wordCount = CountWordsInFile(filePath);
            Console.WriteLine($"Number of words in the file: {wordCount}");
        }
        static int CountWordsInFile(string filePath)
        {
            string fileContent;

            // Read the content of the file
            try
            {
                fileContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
                return -1; // Error code
            }

            // Determine the file format based on the file extension
            string fileExtension = Path.GetExtension(filePath).ToLower();

            switch (fileExtension)
            {
                case ".txt":
                    return CountWordsInPlainText(fileContent);
                case ".csv":
                    return CountWordsInCSV(fileContent);
                case ".json":
                    return CountWordsInJSON(fileContent);
                default:
                    Console.WriteLine("Unsupported file format");
                    return -1; // Error code
            }
        }


           
        static int CountWordsInPlainText(string text)
        {
            // Split the text into words using spaces as delimiters
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        static int CountWordsInCSV(string csvContent)
        {
            // Split the CSV content into words using commas as delimiters
            string[] words = csvContent.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        static int CountWordsInJSON(string jsonContent)
        {
            // Use JSON.NET to parse the JSON content
            try
            {
                JObject jsonObject = JObject.Parse(jsonContent);

                // Extract text values from JSON properties and count words
                int wordCount = 0;
                foreach (var property in jsonObject.Properties())
                {
                    string propertyValue = property.Value.ToString();
                    string[] words = Regex.Split(propertyValue, @"\W+");
                    wordCount += words.Length;
                }

                return wordCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return -1; // Error code
            }
        }
    }
}