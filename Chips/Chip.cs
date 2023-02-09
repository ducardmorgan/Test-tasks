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

        private int[]? p_arrayChips;
        private int steps, count;
        public int[] arrayChips {
            get { return p_arrayChips; }
            set { 
                p_arrayChips = value;
                steps = count = 0;
                for (int i = 0; i < p_arrayChips.Length; i++)
                    count+= p_arrayChips[i];
                count /= p_arrayChips.Length;
            }
        }

        bool CheckCloseLeft(int pos)
        {
            if (p_arrayChips[pos] < count)
                return true;
            return false;
        }

        bool CheckCloseRight(int pos)
        {
            if (p_arrayChips[pos] < count)
                return true;
            return false;
        }


        void Move(int pos_A, int pos_B)
        {
            int stepsIn = p_arrayChips[pos_A] - count;
            p_arrayChips[pos_A]-=stepsIn;
            p_arrayChips[pos_B]+=stepsIn;
            steps+=stepsIn;
        }

        

        public string GetResult()
        {
            while (p_arrayChips.Max() != count)
            {
                int i = p_arrayChips.ToList().IndexOf(p_arrayChips.Max());
                int indexOfLeft = 0, indexOfRight = 0;
                if (i == 0)
                {
                    indexOfRight = 1;
                    indexOfLeft = p_arrayChips.Length - 1;
                }
                else if (i == p_arrayChips.Length - 1)
                {
                    indexOfLeft = i - 1;
                    indexOfRight = 0;
                }
                else
                {
                    indexOfRight = i + 1;
                    indexOfLeft = i - 1;
                }
                while (p_arrayChips[i] > count)
                {
                    if(CheckCloseLeft(indexOfLeft)) Move(i, indexOfLeft);
                    else if (CheckCloseRight(indexOfRight)) Move(i,indexOfRight);
                    else Move(i, indexOfLeft);
                }
            }
            return $"Steps ={steps}";
        }
    }
}
