
namespace Ex2
{
    internal class Searcher(Dictionary<string, List<int>> Data, string Query)
    {
        private readonly Dictionary<string, List<int>> _data = Data;
        private readonly string _query = Query;

        public List<int> ExecuteQuery()
        {
            string[] tokens = _query.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Stack<List<int>> stack = [];
            Stack<string> operators = [];
            foreach (string token in tokens)
            {
                if (token == "AND" || token == "OR")
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(token))
                        ExecuteOperation(stack, operators);
                    operators.Push(token);
                }
                else
                    stack.Push(_data[token]);
            }
            while (operators.Count > 0)
                ExecuteOperation(stack, operators);
            return stack.Peek();
        }

        public static int Priority(string op)
        {
            return op switch
            {
                "AND" => 2,
                "OR" => 1,
                _ => 0
            };
        }

        public void ExecuteOperation(Stack<List<int>> stack, Stack<string> operators)
        {
            string op = operators.Pop();
            List<int> operand2 = stack.Pop();
            List<int> operand1 = stack.Pop();

            if (op == "AND")
            {
                stack.Push(Intersect(operand1, operand2));
            }
            else if (op == "OR")
            {
                stack.Push(Union(operand1, operand2));
            }
        }

        public List<int> Intersect(List<int> list1, List<int> list2)
        {
            List<int> result = [];
            foreach (int item in list1)
            {
                if (list2.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<int> Union(List<int> list1, List<int> list2)
        {
            List<int> list3 = _data["python"];//лежат все доки где есть питон

            List<int> result = new(list1);
            foreach (int item2 in list3)
            {
                if (!list1.Contains(item2))
                {
                    if (list2.Contains(item2))
                        result.Add(item2);
                }

            }
            return result;
        }
    }
}
