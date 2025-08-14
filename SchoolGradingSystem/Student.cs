namespace SchoolGradingSystem
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }

        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A (Excellent)",
                >= 70 and <= 79 => "B (Good)",
                >= 60 and <= 69 => "C (Satisfactory)",
                >= 50 and <= 59 => "D (Needs Improvement)",
                _ => "F (Fail)"
            };
        }

        public string GetPerformanceLevel()
        {
            return Score switch
            {
                >= 90 => "Outstanding",
                >= 80 => "Excellent",
                >= 70 => "Good",
                >= 60 => "Satisfactory",
                >= 50 => "Below Average",
                _ => "Failing"
            };
        }
    }
}
