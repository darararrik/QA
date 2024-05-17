


namespace Ex4
{
    internal class SearchDocument
    {
        private int minWordCount;
        private int maxWordCount;

    // Метод для вычисления минимального и максимального количества слов среди документов
    public void CalculateMinMaxWordCount(IEnumerable<Document> documents)
    {
        if (documents == null || !documents.Any())
        {
            throw new ArgumentException("Document list cannot be null or empty");
        }
        //для нахождения min\max
        minWordCount = documents.Min(doc => doc.GetWordCount());
        maxWordCount = documents.Max(doc => doc.GetWordCount());
    }

    // Весовая функция для оценки релевантности документов с учетом их длины
    public double CalculateRelevance(Document document, double preference)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        // Получаем количество слов в документе
        int documentWordCount = document.GetWordCount();

        // Нормализуем количество слов в документе в диапазоне от 0 до 1
        double normalizedLength = (double)(documentWordCount - minWordCount) / (maxWordCount - minWordCount);
        normalizedLength = Math.Max(0, Math.Min(1, normalizedLength)); // Ограничим значение от 0 до 1

        // Вычисляем вес документа в зависимости от предпочтений пользователя
        if (preference < 0 || preference > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(preference), "Preference should be between 0 and 1");
        }

        // Вес рассчитывается как линейная комбинация нормализованной длины и её дополнения
        double weight = preference * normalizedLength + (1 - preference) * (1 - normalizedLength);
        return weight;
    }
    }
}
