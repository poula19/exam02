using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public abstract class Exam : ICloneable
    {
        public TimeSpan Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }

        protected Exam(TimeSpan time, int numberOfQuestions)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Questions = new List<Question>();
        }

        public abstract void ShowExam();

        public abstract object Clone();
    }
}
