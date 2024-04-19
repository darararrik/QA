#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main() {
    int n;
    cin >> n;
    vector<int> grades(n);
    for (int i = 0; i < n; ++i) {
        cin >> grades[i];
    }

    int left = 0, right = 0;
    int maxCount = 0, count = 0;

    while (right < n) {
        if (grades[right] == 5)
            count++;

        while (left <= right && (grades[right] == 2 || grades[right] == 3)) {
            if (grades[left] == 5)
                count--;
            left++;
        }

        maxCount = max(maxCount, count);
        right++;
    }

    if (maxCount == 0)
        cout << "-1\n";
    else
        cout << maxCount << endl;

    return 0;
}
