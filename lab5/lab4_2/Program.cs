using Lucene.Net.Util;
using System.Runtime.Intrinsics.Arm;
using static Program.Intersections;
internal class Program
{
    /*Алгоритм Дейкстры: 
     * Максимальное расстояние: Для каждого перекрестка найдите максимальное расстояние от перекрестка до ближайшего депо.
     * Выбор оптимального местоположения: Выберите перекресток с минимальным максимальным расстоянием из шага 2. 
     * Это будет оптимальное местоположение для нового пожарного депо.
     * Размещение нового депо: Разместите новое пожарное депо на выбранном перекрестке*/
    private static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());
        while (t-- > 0)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int f = int.Parse(inputs[0]);// кол-во депо
            int I = int.Parse(inputs[1]);// кол-во перекрестков
            int OptimalDistance = 0;
            List<Intersections> intersections = new List<Intersections>();
           
            int[] DepoNumber = new int[f];
            for(int i=0;i<f;i++)
            {
                DepoNumber[i] = int.Parse(Console.ReadLine()) - 1;
            }
            string[] line;
            for (int i = 0; i < I; i++)
            {
                line = Console.ReadLine().Split(' ');
                intersections.Add(new Intersections(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2])));
            }
            for (int i = 0;i<f;i++)
            {
                intersections[DepoNumber[i]].hasDepo = true;

            }

                (int optimalIntersectionNumber, int optimalDistance) = Dijkstra(intersections, f, I, DepoNumber);
            Console.WriteLine("Лучшее местоположение для депо: " + optimalIntersectionNumber);


        }
        Console.ReadLine();
    }
    private static (int IntersectionNumber, int MaxDistance) Dijkstra(List<Intersections> intersections, int f, int I, int[] DepoNumber)
    {
        // Создаем массив расстояний до каждого перекрестка и инициализируем его бесконечностью
        int[] distances = new int[I];
        for (int i = 0; i < I; i++)
        {
            distances[i] = int.MaxValue;
        }

        // Массив флагов, чтобы отслеживать, был ли уже посещен перекресток
        bool[] visited = new bool[I];

        // Добавляем фиктивное депо на все перекрестки, где оно не было установлено
        foreach (int depo in DepoNumber)
        {
            intersections[depo].hasDepo = true;
            distances[depo] = 0; // Расстояние от депо до самого себя равно 0
        }

        // Запускаем алгоритм Дейкстры
        while (true)
        {
            int minDistance = int.MaxValue;
            int minIndex = -1;

            // Находим перекресток с минимальным расстоянием, который еще не был посещен
            for (int i = 0; i < I; i++)
            {
                if (!visited[i] && distances[i] < minDistance)
                {
                    minDistance = distances[i];
                    minIndex = i;
                }
            }

            // Если все перекрестки были посещены, выходим из цикла
            if (minIndex == -1)
                break;

            visited[minIndex] = true;

            // Обновляем расстояния до соседа текущего перекрестка
            int neighbor = intersections[minIndex].Neighbor;
            int distanceToNeighbor = intersections[minIndex].Distance;
            int newDistance = distances[minIndex] + distanceToNeighbor;
            if (newDistance < distances[neighbor])
            {
                distances[neighbor] = newDistance;
            }
        }

        // Находим максимальное расстояние от фиктивного депо до всех перекрестков
        int maxDistance = int.MinValue;
        int optimalIntersectionNumber = -1; // Номер перекрестка с оптимальным расположением депо
        for (int i = 0; i < I; i++)
        {
            if (!intersections[i].hasDepo) // Пропускаем уже существующие депо
            {
                if (distances[i] > maxDistance)
                {
                    maxDistance = distances[i];
                    optimalIntersectionNumber = i + 1; // Номер перекрестка на единицу больше индекса
                }
            }
        }

        return (optimalIntersectionNumber, maxDistance);
    }


    public class Intersections
    {
        public int Number;
        public int Neighbor;
        public int Distance;
        public bool hasDepo;

       public Intersections(int Number, int Neighbor, int Distance) 
        {
            this.Number = Number;
            this.Neighbor = Neighbor;
            this.Distance = Distance;
            this.hasDepo = false;
        }

    }
}
