using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chips
{
    internal class Chip
    {
        public Chip() { }

        private int[]? p_arrayChips;// массив с которым будет работать класс
        private int steps, count;//счетсчик шагов и среднее арифметическое массива
        public int[] arrayChips {
            get { return p_arrayChips; }
            set { 
                p_arrayChips = value; //присваем значение массива
                steps = count = 0;// чистка переменных информации от старого массива
                for (int i = 0; i < p_arrayChips.Length; i++)
                    count+= p_arrayChips[i];
                count /= p_arrayChips.Length;// получение сред. арифмет. нового массива
            }
        }

        bool CheckClose(int pos)//проверка ближнего элемента массива
        {
            if (p_arrayChips[pos] < count)
                return true;
            return false;
        }


        void Move(int pos_A, int pos_B)//передвижение фишек и ведение счёта шагов
        {
            p_arrayChips[pos_A]--;
            p_arrayChips[pos_B]++;
            steps++;
        }

        

        public string GetResult()
        {
            while (p_arrayChips.Max() != count)//пока максимальный элемент не будет равен среднему занчению
            {
                int i = p_arrayChips.ToList().IndexOf(p_arrayChips.Max());//индекс максимального элемента
                int j = p_arrayChips.ToList().IndexOf(p_arrayChips.Min());//индекс минимального элемента
                int len = p_arrayChips.Length;//длина массива
                int indexOfLeft = 0, indexOfRight = 0;//индексы ближних элементов 
                if (i == 0)//присвоение индексов ближних элементов,если максимальный элемент первый
                {
                    indexOfRight = 1;
                    indexOfLeft = len - 1;
                }
                else if (i == len - 1)//присвоение индексов ближних элементов,если максимальный элемент последний
                {
                    indexOfLeft = i - 1;
                    indexOfRight = 0;
                }
                else//присвоение индексов ближних элементов,если максимальный элемент не на границе массива
                {
                    indexOfRight = i + 1;
                    indexOfLeft = i - 1;
                }
                while (p_arrayChips[i] > count)//работа с маскимальным элементом, пока не будет равен ср.зн.
                {
                    if(CheckClose(indexOfLeft)) Move(i, indexOfLeft);//проверка элемента слева
                    else if (CheckClose(indexOfRight)) Move(i,indexOfRight);//проверка элемента справа
                    else if(i > j)
                    {
                        if ((i + 1) - (j + 1) > p_arrayChips.Length - (i + 1) + j) Move(i, indexOfRight);//если до минимального путь короче через элемент справа
                        else Move(i, indexOfLeft);//если до минимального путь короче через элемент слева
                    }
                    else
                    {
                        if ((j + 1) - (i + 1) > p_arrayChips.Length - (j + 1) + i) Move(i, indexOfLeft);//если до минимального путь короче через элемент слева
                        else Move(i, indexOfRight);//если до минимального путь короче через элемент справа
                    }
                }
            }
            return $"Steps ={steps}";//вывод кол-во шагов для решения
        }
    }
}
