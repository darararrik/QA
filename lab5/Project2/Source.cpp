#include <iostream>
#include <vector>
#include <queue>
#include <unordered_map>
#include <climits>

using namespace std;

typedef pair<int, int> pii; // Пара (расстояние, вершина)

// Реализация алгоритма Дейкстры
vector<int> dijkstra(unordered_map<int, unordered_map<int, int>>& graph, int start) {
    vector<int> distances(graph.size() + 1, INT_MAX);
    distances[start] = 0;
    priority_queue<pii, vector<pii>, greater<pii>> pq;
    pq.push({ 0, start });

    while (!pq.empty()) {
        int u = pq.top().second;
        int d = pq.top().first;
        pq.pop();

        if (d > distances[u]) continue;

        for (auto& neighbor : graph[u]) {
            int v = neighbor.first;
            int weight = neighbor.second;

            if (distances[u] + weight < distances[v]) {
                distances[v] = distances[u] + weight;
                pq.push({ distances[v], v });
            }
        }
    }

    return distances;
}

// Функция для нахождения оптимального местоположения нового пожарного депо
int minFireStationLocation(int num_depots, int num_intersections, vector<int>& depots, unordered_map<int, unordered_map<int, int>>& graph) {
    // Создаём фиктивную вершину, соединённую со всеми существующими пожарными депо
    for (int depot : depots) {
        graph[0][depot] = 0;
    }

    int min_max_distance = INT_MAX;
    int optimal_location = 1;

    // Запускаем алгоритм Дейкстры из фиктивной вершины
    vector<int> distances = dijkstra(graph, 0);

    // Находим вершину с максимальным расстоянием до ближайшего пожарного депо
    for (int intersection = 1; intersection <= num_intersections; ++intersection) {
        if (distances[intersection] > min_max_distance) {
            min_max_distance = distances[intersection];
            optimal_location = intersection;
        }
    }

    return optimal_location;
}

int main() {
    int num_test_cases;
    cin >> num_test_cases;

    while (num_test_cases--) {
        int num_depots, num_intersections;
        cin >> num_depots >> num_intersections;

        unordered_map<int, unordered_map<int, int>> graph;

        // Заполняем граф информацией о дорогах
        for (int i = 0; i < num_intersections; ++i) {
            int intersection;
            cin >> intersection;

            int neighbor, distance;
            while (cin >> neighbor && neighbor != 0) {
                cin >> distance;
                graph[intersection][neighbor] = distance;
            }
        }

        // Считываем информацию о расположении существующих пожарных депо
        vector<int> depots(num_depots);
        for (int i = 0; i < num_depots; ++i) {
            cin >> depots[i];
        }

        // Находим оптимальное местоположение нового пожарного депо
        int optimal_location = minFireStationLocation(num_depots, num_intersections, depots, graph);
        cout << optimal_location << endl;

        if (num_test_cases > 1) {
            cout << endl; // Вывод пустой строки между тестовыми блоками
        }
    }

    return 0;
}
