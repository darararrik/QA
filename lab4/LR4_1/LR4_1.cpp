#include <iostream>
#include <vector>
#include <string>
#include <algorithm>

using namespace std;

const int N = 4; // Размеры головоломки (4x4)
const int dx[] = { 0, 0, -1, 1 }; // Смещения для движения пустой клетки (влево, вправо, вверх, вниз)
const int dy[] = { -1, 1, 0, 0 };

bool is_valid(int x, int y) {
    return x >= 0 && x < N && y >= 0 && y < N;
}

bool is_goal(const vector<vector<int>>& puzzle) {
    int value = 1;
    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            if (i == N - 1 && j == N - 1) {
                if (puzzle[i][j] != 0) {
                    return false;
                }
            }
            else {
                if (puzzle[i][j] != value) {
                    return false;
                }
            }
            ++value;
        }
    }
    return true;
}

void print_moves(const vector<string>& moves) {
    for (const string& move : moves) {
        cout << move << " ";
    }
    cout << endl;
}

void backtrack(vector<vector<int>>& puzzle, int x, int y, vector<string>& moves) {
    if (is_goal(puzzle)) {
        print_moves(moves);
        return;
    }

    for (int i = 0; i < 4; ++i) {
        int nx = x + dx[i];
        int ny = y + dy[i];
        if (is_valid(nx, ny)) {
            swap(puzzle[x][y], puzzle[nx][ny]);
            moves.push_back(string(1, "LRUD"[i])); // Преобразование индекса движения в строку (L - влево, R - вправо, U - вверх, D - вниз)
            backtrack(puzzle, nx, ny, moves);
            moves.pop_back(); // Отмена хода
            swap(puzzle[x][y], puzzle[nx][ny]);
        }
    }
}

int main() {
    int t;
    cin >> t; // Число тестовых примеров
    while (t--) {
        vector<vector<int>> puzzle(N, vector<int>(N));
        for (int i = 0; i < N; ++i) {
            for (int j = 0; j < N; ++j) {
                cin >> puzzle[i][j];
            }
        }

        // Находим координаты пустой клетки (нуля)
        int empty_x, empty_y;
        for (int i = 0; i < N; ++i) {
            for (int j = 0; j < N; ++j) {
                if (puzzle[i][j] == 0) {
                    empty_x = i;
                    empty_y = j;
                    break;
                }
            }
        }

        vector<string> moves;
        backtrack(puzzle, empty_x, empty_y, moves);
    }

    return 0;
}
