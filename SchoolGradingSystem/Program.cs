using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGradingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Academic Performance Evaluation System");
                Console.WriteLine(new string('=', 45));
                
                StudentResultProcessor processor = new StudentResultProcessor();

                Console.Write("Enter student data file path: ");
                string inputPath = Console.ReadLine();

                Console.Write("Enter report output file path: ");
                string outputPath = Console.ReadLine();

                Console.WriteLine("\nProcessing student records...");
                List<Student> students = processor.ReadStudentsFromFile(inputPath);
                
                Console.WriteLine($"Successfully processed {students.Count} student records");
                
                processor.WriteReportToFile(students, outputPath);
                Console.WriteLine($"Academic report generated: {outputPath}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine("Score Error: " + ex.Message);
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine("Data Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }
    }
}
