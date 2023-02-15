using Chips;
Chip chip = new Chip();

//Данные для ввода
// 1, 5, 9, 10, 5
// 1, 2, 3 
// 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 
// 6, 2, 4, 10, 3
// 0, 10, 0, 8, 3, 10, 7, 0, 9, 3 
// 2, 7,4,2,4,10,5,7,2,7


int numTest = 1;
again:
try
{
    Console.WriteLine($"\nТест №{numTest}");
    Console.WriteLine("Введите массив чисел:");

    string[] userString = Console.ReadLine().Replace(" ", "").Split(",");
    int[] array = new int[userString.Length];
    for (int i = 0; i < array.Length; i++)
        array[i] = int.Parse(userString[i]);

    chip.ArrayChips = array;
    chip.СountTheSteps();
    Console.WriteLine($"Шагов для решения:{chip.Steps}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
numTest++;
goto again;




