using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class FinalExam : Exam
    {
        public FinalExam(TimeSpan time, int numberOfQuestions) : base(time, numberOfQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("Final Exam:");
            foreach (var question in Questions)
            {
                Console.WriteLine(question);
                foreach (var answer in question.AnswerList)
                {
                    Console.WriteLine(answer);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Total Grade: {Questions.Sum(q => q.Mark)}");
        }

        public override object Clone()
        {
            return new FinalExam(Time, NumberOfQuestions)
            {
                Questions = this.Questions.Select(q => (Question)q.Clone()).ToList()
            };
        }
    }
}
