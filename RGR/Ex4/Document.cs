using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex4
{
    internal class Document
    {

        public string Title { get; set; }
        public string Content { get; set; }

        public int GetWordCount()
        {
            //Проверка на null
            if (string.IsNullOrWhiteSpace(Content))
            {
                return 0;
            }
            //StringSplitOptions.RemoveEmptyEntries указывает, что метод должен удалять пустые элементы из результирующего массива после разделения.
            return Content.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

}
