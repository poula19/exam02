using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class MCQQuestion : Question
    {
        public MCQQuestion(string header, string body, int mark) : base(header, body, mark) { }

        public override object Clone()
        {
            return new MCQQuestion(Header, Body, Mark)
            {
                CorrectAnswer = this.CorrectAnswer,
                AnswerList = this.AnswerList.Select(a => new Answer(a.AnswerId, a.AnswerText)).ToList()
            };
        }
    }
}
