using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryy
{
    public class Cube
    {
        const int N = 3;
        public string state { get; set; }
        public eColors[,] Front { get; set; }
        public eColors[,] Back { get; set; }
        public eColors[,] Up { get; set; }
        public eColors[,] Down { get; set; }
        public eColors[,] Right { get; set; }
        public eColors[,] Left { get; set; }
        public eColors[][,] bigArray { get; set; }
        public string a_str { get; set; }
        public Cube()

        {
            Front = new eColors[N, N];
            Back = new eColors[N, N];
            Up = new eColors[N, N];
            Down = new eColors[N, N];
            Right = new eColors[N, N];
            Left = new eColors[N, N];
            bigArray = new eColors[6][,]                ;
            bigArray[0] = Up;
            bigArray[1] = Left;
            bigArray[2] = Front;
            bigArray[3] = Right;
            bigArray[4] = Back;
            bigArray[5] = Down;
           
            InitCube();
        }

        public Cube(Cube c1)

        {
            Front = new eColors[N, N];
            Back = new eColors[N, N];
            Up = new eColors[N, N];
            Down = new eColors[N, N];
            Right = new eColors[N, N];
            Left = new eColors[N, N];
            Front = c1.Front;
            Back = c1.Back;
            Up = c1.Up;
            Down = c1.Down;
            Left = c1.Left;
            Right = c1.Right;
            bigArray = new eColors[6][,];
            bigArray[0] = Up;
            bigArray[1] = Left;
            bigArray[2] = Front;
            bigArray[3] = Right;
            bigArray[4] = Back;
            bigArray[5] = Down;
        }

        public void InitCube()
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    bigArray[0][i, j] = eColors.white;
                    bigArray[1][i, j] = eColors.green;
                    bigArray[2][i, j] = eColors.red;
                    bigArray[3][i, j] = eColors.blue;
                    bigArray[4][i, j] = eColors.orange;
                    bigArray[5][i, j] = eColors.yellow;
                   
                }
        }
        public void TurnLeft(eColors[,] mat)
        {
            eColors[,] temp = new eColors[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    temp[j, N - i - 1] = mat[i, j];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    mat[i, j] = temp[i, j];
        }
        public void TurnRight(eColors[,] mat)
        {
            eColors[,] temp = new eColors[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    temp[i, j] = mat[j, N - i - 1];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    mat[i, j] = temp[i, j];
        }
        public void horizontal_twist(int direction, int row)
        {
            //הפונקציה מקבלת מטריצה התחלתית שורה במטריצה לסיבוב ואת המטריצה הגדולה שמכילה את כל מטריצות הקוביה
            // הפונקציה תסובב את השורה  
            if (row >= bigArray[2].Length || row < 0)
                return;
            if (direction != 0 && direction != 1)
                return;
            eColors[] temp = new eColors[N];
            for (int j = 0; j < N; j++)
                temp[j] = bigArray[2][row, j];
            if (direction == 0)//Twist left
            {
                for (int i = 0; i < N; i++)
                {
                    bigArray[2][row, i] = bigArray[3][row, i];
                    bigArray[3][row, i] = bigArray[4][row, i];
                    bigArray[4][row, i] = bigArray[1][row, i];
                    bigArray[1][row, i] = temp[i];
                }
                if (row == 0)
                    TurnLeft(bigArray[0]);
                if (row == 2)
                    TurnLeft(bigArray[5]);

            }
            else if (direction == 1) //Twist right
            {
                for (int i = 0; i < N; i++)
                {
                    bigArray[2][row, i] = bigArray[1][row, i];
                    bigArray[1][row, i] = bigArray[4][row, i];
                    bigArray[4][row, i] = bigArray[3][row, i];
                    bigArray[3][row, i] = temp[i];
                }
                if (row == 0)
                    TurnRight(bigArray[0]);
                if (row == 2)
                    TurnRight(bigArray[5]);

            }
        }

     
        public bool isSolvedCube(string str)
        {

            for (int i = 0; i < str.Length; i++)
            {
                if (i < 9) if (str[i] != 'w')
                        return false;
                if (i >= 9 && i < 18) if (str[i] != 'g')
                        return false;
                if (i >= 18 && i < 27) if (str[i] != 'r')
                        return false;
                if (i >= 27 && i < 36) if (str[i] != 'b')
                        return false;
                if (i >= 36 && i < 45) if (str[i] != 'o')
                        return false;
                if (i >= 45 && i < 54) if (str[i] != 'y')
                        return false;
            }
            return true;
        }

        //public void strToMat()
        //{
        //    for (int i = 0; i < this.state.Length; i++)
        //    {
        //        if(i<9)
        //            this.Up[][]
        //    }
        //}

        public string matToStr()
        {
            string str = "";
            for (int y = 0; y < 6; y++)
                for (int i = 0; 
                    i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if (this.bigArray[y][i, j] == eColors.white)
                            str += "w";
                        else if (bigArray[y][i, j] == eColors.green)
                            str += "g";
                        else if (bigArray[y][i, j] == eColors.red)
                            str += "r";
                        else if (bigArray[y][i, j] == eColors.blue)
                            str += "b";
                        else if (bigArray[y][i, j] == eColors.orange)
                            str += "o";
                        else if (bigArray[y][i, j] == eColors.yellow)
                            str += "y";
                    }
            return str;
        }
        public void vertical_twist( int direction, int column)
        {
            if (column >= N || column < 0)
                return;
            if (direction != 0 && direction != 1)
                return;
            for (int i = 0; i < N; i++)

            {
                eColors temp = bigArray[0][i, column];
                if (direction == 0)
                {
                    bigArray[0][i, column] = bigArray[4][N - i - 1, N-column-1];
                    bigArray[4][N-i-1, N-column-1] = bigArray[5][ i , column];
                    bigArray[5][i, column] = bigArray[2][i, column];
                    bigArray[2][i, column] = temp;
                }
                
                else
                {
                    bigArray[0][i, column] = bigArray[2][i, column];
                    bigArray[2][i, column] = bigArray[5][i, column];
                    bigArray[5][i, column] = bigArray[4][N-i-1, N-column-1];
                    bigArray[4][N-i-1, N-column-1] = temp;
                    
                }

            }
            if (direction == 0)
            {
                if (column == 0)
                    TurnLeft(bigArray[1]);
                if (column == 2)
                    TurnRight(bigArray[3]);
            }
            else if (direction == 1)
            {
                if (column == 0)
                    TurnRight(bigArray[1]);
                if (column == 2)
                    TurnLeft(bigArray[3]);
            }
        }
        public void side_twist(int direction, int column)
        {
            for (int i = 0; i < N; i++)
            {
                eColors temp = bigArray[0][column, i];
                if (direction == 0)
                {
                    bigArray[0][column, i] = bigArray[3][i, N - column - 1];
                    bigArray[3][i, N - column - 1] = bigArray[5][N - column - 1, N - 1 - i];
                    bigArray[5][N - column - 1, N - 1 - i] = bigArray[1][N - i - 1, column];
                    bigArray[1][N - i - 1, column] = temp;
                }
                else
                {
                    bigArray[0][column, i] = bigArray[1][N - i - 1, column];
                    bigArray[1][N - i - 1, column] = bigArray[5][N - column - 1, N - 1 - i];
                    bigArray[5][N - column - 1, N - 1 - i] = bigArray[3][i, N - column - 1];
                    bigArray[3][i, N - column - 1] = temp;
                }

            }
            if (direction == 0)
            {
                if (column == 0)
                    TurnLeft(bigArray[4]);
                if (column == 2)
                    TurnLeft(bigArray[2]);
            }
            else if (direction == 1)
            {
                if (column == 0)
                    TurnRight(bigArray[4]);
                if (column == 2)
                    TurnRight(bigArray[2]);
            }
        }

    }

}