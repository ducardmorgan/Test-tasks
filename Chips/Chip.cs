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
        private char[]? p_regChips;
        private int p_steps, p_count;//счетсчик шагов и среднее арифметическое массива
        public int[] ArrayChips {
            get { return p_arrayChips; }
            set { 
                p_arrayChips = value; //присваем значение массива
                p_regChips = new char[value.Length];
                p_steps = p_count = 0;// чистка переменных информации от старого массива
                for (int i = 0; i < p_arrayChips.Length; i++)
                {
                    p_regChips[i] = 'X';
                    p_count += p_arrayChips[i];
                }
                p_count /= p_arrayChips.Length;// получение сред. арифмет. нового массива
            }
        }

        public int Steps
        {
            get { return p_steps; }
        }
        bool CheckClose(int pos)//проверка ближнего элемента массива
        {
            if (p_arrayChips[pos] < p_count)
                return true;
            return false;
        }


        void Move(int pos_A, int pos_B, int step)//передвижение фишек и ведение счёта шагов
        {
            p_arrayChips[pos_A]-=step;
            p_arrayChips[pos_B]+=step;
            p_steps += step;
        }

        void HardMove(int indexOfMax, int indexOfMin, int countLeftSide, int countRightSide , int indexOfRight, int indexOfLeft, int length)
        {
            int i;
            int step, stepOtherSide;
            step = stepOtherSide = p_arrayChips[indexOfMin];
            if (countLeftSide == countRightSide)//количество элементов массива, которые нужно будет пройти до минимального равны
            {
                #region Счёт_сколько_фишек_не_хватает
                i = indexOfRight;
                while (i != indexOfMin)
                {
                    step += p_arrayChips[i];
                    if (i == length - 1) i = 0;
                    else i++;
                }

                i = indexOfLeft;
                while (i != indexOfMin)
                {
                    stepOtherSide += p_arrayChips[i];
                    if (i == 0) i = length - 1;
                    else i--;
                }

                step = countRightSide * p_count - step;
                stepOtherSide = countLeftSide * p_count - stepOtherSide;
                #endregion


                if (step > stepOtherSide)//если справа не хватает фише больше
                {
                    
                    if (p_arrayChips[indexOfMax] - p_count < step) step = p_arrayChips[indexOfMax] - p_count;
                    p_regChips[indexOfMax] = 'R';
                    Move(indexOfMax, indexOfRight, step);
                }
                else
                {
                    
                    if (p_arrayChips[indexOfMax] - p_count < stepOtherSide) stepOtherSide = p_arrayChips[indexOfMax] - p_count;
                    p_regChips[indexOfMax] = 'L';
                    Move(indexOfMax, indexOfLeft, stepOtherSide);
                }


            }
            else if (countLeftSide > countRightSide && p_regChips[indexOfRight] != 'L')//если до минимального путь короче через элемент справа
            {
                i = indexOfRight;
                while (i != indexOfMin)
                {
                    step += p_arrayChips[i];
                    if (i == length - 1) i = 0;
                    else i++;
                }
                step = countRightSide * p_count - step;
                if (p_arrayChips[indexOfMax] - p_count < step) step = p_arrayChips[indexOfMax] - p_count;
                p_regChips[indexOfMax] = 'R';
                Move(indexOfMax, indexOfRight, step);
            }
            else if (p_regChips[indexOfLeft] != 'R')//если до минимального путь короче через элемент слева
            {
                i = indexOfLeft;
                while (i != indexOfMin)
                {
                    step += p_arrayChips[i];
                    if (i == 0) i = length - 1;
                    else i--;
                }
                step = countLeftSide * p_count - step;
                if (p_arrayChips[indexOfMax] - p_count < step) step = p_arrayChips[indexOfMax] - p_count;
                p_regChips[indexOfMax] = 'L';
                Move(indexOfMax, indexOfLeft, step);
            }
        }

        public void СountTheSteps()
        {
            while (p_arrayChips.Max() != p_count)//пока максимальный элемент не будет равен среднему занчению
            {
                int step, stepsLeftSide, stepsRightSide;
                int indexOfMax = p_arrayChips.ToList().IndexOf(p_arrayChips.Max());//индекс максимального элемента
                int indexOfMin = p_arrayChips.ToList().IndexOf(p_arrayChips.Min());//индекс минимального элемента
                int len = p_arrayChips.Length;//длина массива
                int indexOfLeft = 0, indexOfRight = 0;//индексы ближних элементов 
                if (indexOfMax == 0)//присвоение индексов ближних элементов,если максимальный элемент первый
                {
                    indexOfRight = 1;
                    indexOfLeft = len - 1;
                }
                else if (indexOfMax == len - 1)//присвоение индексов ближних элементов,если максимальный элемент последний
                {
                    indexOfLeft = indexOfMax - 1;
                    indexOfRight = 0;
                }
                else//присвоение индексов ближних элементов,если максимальный элемент не на границе массива
                {
                    indexOfRight = indexOfMax + 1;
                    indexOfLeft = indexOfMax - 1;
                }
                while (p_arrayChips[indexOfMax] > p_count)//работа с маскимальным элементом, пока не будет равен ср.зн.
                {
                    step = 1;

                    if (indexOfMax > indexOfMin)//если максимальный стоит после минимального
                    {
                        stepsLeftSide = (indexOfMax + 1) - (indexOfMin + 1);//путь слева
                        stepsRightSide = p_arrayChips.Length - (indexOfMax + 1) + indexOfMin + 1;//путь справа
                    }
                    else
                    {
                        stepsLeftSide = p_arrayChips.Length - (indexOfMin + 1) + indexOfMax + 1;//путь слева
                        stepsRightSide = (indexOfMin + 1) - (indexOfMax + 1);//путь справа
                    }


                    if (CheckClose(indexOfLeft) && CheckClose(indexOfRight))//проверка если двум соседним нужны фишки
                    {
                        if (p_count - p_arrayChips[indexOfLeft] <= p_count - p_arrayChips[indexOfRight] || stepsLeftSide < stepsRightSide)
                        {//если левой стороне нужно меньше фишек, чем правой или слева путь до минимальноой короче,чем справа
                            Move(indexOfMax, indexOfLeft, step);
                            p_regChips[indexOfMax] = 'L';
                        }
                        else
                        {
                            Move(indexOfMax, indexOfRight, step);
                            p_regChips[indexOfMax] = 'R';
                        }
                    }
                    else if (CheckClose(indexOfLeft))
                    {
                        Move(indexOfMax, indexOfLeft, step);
                        p_regChips[indexOfMax] = 'L';
                    }
                    else if (CheckClose(indexOfRight))
                    {
                        Move(indexOfMax, indexOfRight, step);
                        p_regChips[indexOfMax] = 'R';
                    }
                    else
                    {
                        HardMove(indexOfMax, indexOfMin, stepsLeftSide, stepsRightSide, indexOfRight, indexOfLeft, len);
                        break;
                    }
                }
            }
        }
    }
}
