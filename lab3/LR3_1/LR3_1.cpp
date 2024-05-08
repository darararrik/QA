#include <stdio.h>
#include <iostream>


int main()
{
    setlocale(LC_ALL, "rus");
    int a, b;
    printf("Введите два целых числа.\n");
    printf("Для завершения программы введите \"0 0\".\n");

    while (scanf_s("%d%d", &a, &b) == 2)
    {
        printf("\nВы ввели %d и %d.\n", a, b);

        if (!a && !b)
        {
            printf("Программа завершена.\n");
            return 0;
        }
        int c = 0, ans = 0;
        for (int i = 9; i >= 0; i--)
        {
            c = (a % 10 + b % 10 + c) > 9 ? 1 : 0;
            ans += c;
            a /= 10; b /= 10;

        }
        if (ans == 0) printf("Нету переносов.\n");
        else printf("Всего переносов: %d.\n\n", ans);
        printf("Введите два целых числа.\n");
        printf("Для завершения программы введите \"0 0\".\n");


    }
    if (scanf_s("%d%d", &a, &b) != 2)
    {
        printf("Вы ввели не числа.\n");
        printf("Программа завершена.\n");
        return 0;

    }

}