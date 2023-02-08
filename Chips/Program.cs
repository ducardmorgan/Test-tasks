using Chips;
Chip chip = new Chip();
int[] Test1 = { 1, 5, 9, 10, 5 };
int[] Test2 = { 1, 2, 3 };
int[] Test3 = { 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 };


Console.WriteLine("Test 1:");
chip.arrayChips = Test1;
Console.WriteLine(chip.GetResult());
Console.WriteLine("Test 2:");
chip.arrayChips = Test2;
Console.WriteLine(chip.GetResult());
Console.WriteLine("Test 3:");
chip.arrayChips = Test3;
Console.WriteLine(chip.GetResult());

