using System;
using System.Web;
namespace FinalProject
{
    internal class Document
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> ExternalLinks { get; set; }

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
