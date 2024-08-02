using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class PracticalExam : Exam
    {
        public PracticalExam(TimeSpan time, int numberOfQuestions) : base(time, numberOfQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("Practical Exam:");
            foreach (var question in Questions)
            {
                Console.WriteLine(question);
                foreach (var answer in question.AnswerList)
                {
                    Console.WriteLine(answer);
                }
                Console.WriteLine($"Correct Answer: {question.CorrectAnswer}");
                Console.WriteLine();
            }
        }

        public override object Clone()
        {
            return new PracticalExam(Time, NumberOfQuestions)
            {
                Questions = this.Questions.Select(q => (Question)q.Clone()).ToList()
            };
        }
    }
}
