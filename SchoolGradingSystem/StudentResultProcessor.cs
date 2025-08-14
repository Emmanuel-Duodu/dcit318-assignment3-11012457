using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGradingSystem
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            List<Student> students = new List<Student>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length != 3)
                        throw new MissingFieldException($"Invalid record: {line}");

                    int id;
                    if (!int.TryParse(parts[0].Trim(), out id))
                        throw new FormatException($"Invalid ID in record: {line}");

                    string fullName = parts[1].Trim();
                    if (string.IsNullOrWhiteSpace(fullName))
                        throw new MissingFieldException($"Missing full name in record: {line}");

                    int score;
                    if (!int.TryParse(parts[2].Trim(), out score))
                        throw new InvalidScoreFormatException($"Invalid score in record: {line}");

                    students.Add(new Student { Id = id, FullName = fullName, Score = score });
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("=== ACADEMIC PERFORMANCE REPORT ===");
                writer.WriteLine($"Generated on: {DateTime.Now:MMMM dd, yyyy 'at' HH:mm}");
                writer.WriteLine($"Total Students Evaluated: {students.Count}");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine();

                foreach (var student in students)
                {
                    writer.WriteLine($"Student: {student.FullName} (ID: {student.Id})");
                    writer.WriteLine($"Score: {student.Score}/100 | Grade: {student.GetGrade()}");
                    writer.WriteLine($"Performance: {student.GetPerformanceLevel()}");
                    writer.WriteLine(new string('-', 40));
                }
            }
        }
    }
}
