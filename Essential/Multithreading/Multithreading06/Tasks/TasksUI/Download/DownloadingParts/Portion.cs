using NLog;

namespace TasksUI
{
    public class Portion
    {
        private int count;
        private int contentLength;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public int Number { get; private set; }
        public int Start => GetStartPosition(Number, count, contentLength);
        public int End => GetEndPosition(Number, count, contentLength);
        public int Length => GetLength(Start, End);

        public Portion(int number, int count, int contentLength)
        {
            logger.Trace("Создание новой порции данных.");
            logger.Debug($"Вызов метода {nameof(Portion)}.");

            this.count = count;
            this.contentLength = contentLength;

            Number = number;

            logger.Debug($"Завершение метода {nameof(Portion)}.");
        }

        private int GetStartPosition(int portionNumber, int portionsCount, int contentLength)
        {
            logger.Trace("Получение стартового байта порции.");

            return (contentLength / portionsCount) * portionNumber;
        }

        private int GetEndPosition(int portionNumber, int portionCount, int contentLength)
        {
            logger.Trace("Получение конечного байта порции.");

            return (portionNumber == portionCount - 1) ? contentLength - 1 : Start + contentLength / portionCount - 1;
        }

        private int GetLength(int start, int end)
        {
            logger.Trace("Получение длины порции.");

            return end - start + 1;
        }
    }
}
