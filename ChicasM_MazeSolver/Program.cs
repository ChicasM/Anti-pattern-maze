using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChicasM_MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeGen mapOne = new MazeGen(); // generates the maze, but any maze can be used
            SolveMaze SolveOne = new SolveMaze(mapOne.mazeUnderlay); // pass maze into interface
            Console.ReadLine();
        }
    }

    public static class MyPosition // global use position variables
    {
        public static IntStack pastXStack = new IntStack(100); // where to store x choice coordinate
        public static IntStack pastYStack = new IntStack(100); // where to store y choice coordinate
        public static int x { get; set; }
        public static int y { get; set; }
        public static int xEnd { get; set; }
        public static int yEnd { get; set; }
    }

    public class SolveMaze
    {

        public SolveMaze(char[,] A) // solve a character array
        {
            FindStart start = new FindStart(A); // uses FindStart class to locate 'S' in 2D array
            FindEnd end = new FindEnd(A); // uses FindEnd class to locate 'E' in 2D array
            MyPosition.x = start.Xpoint;
            MyPosition.y = start.Ypoint;
            MyPosition.xEnd = end.Xpoint;
            MyPosition.yEnd = end.Ypoint;

            while (MyPosition.x != MyPosition.xEnd && MyPosition.y != MyPosition.yEnd) // keep going until x and y are the same as end x and y values
            {
                CheckWalls check = new CheckWalls(A);
                Console.Clear();
                PrintArray print = new PrintArray(A);
                Thread.Sleep(300);
            }
            Console.WriteLine("\nThe Maze Has Been Solved!!!");
            Console.ReadLine();

        }
    }

    interface Point
    {
        int Xpoint { get; set; }
        int Ypoint { get; set; }
    }

    public class FindStart : Point // looks for S in 2D array
    {
        public int Xpoint { get; set; }
        public int Ypoint { get; set; }
        public FindStart(char[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++) //GetLength 0 will get rows
            {
                for (int j = 0; j < A.GetLength(1); j++) //GetLength 1 will get columns
                {
                    if (A[i, j] == 'S')
                    {
                        Xpoint = i;
                        Ypoint = j;
                        break;
                    }
                }
            }
        }
    }

    public class FindEnd : Point // looks for E in 2D array
    {
        public int Xpoint { get; set; }
        public int Ypoint { get; set; }
        public FindEnd(char[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++) //GetLength 0 will get rows
            {
                for (int j = 0; j < A.GetLength(1); j++) //GetLength 1 will get columns
                {
                    if (A[i, j] == 'E')
                    {
                        Xpoint = i;
                        Ypoint = j;
                        break;
                    }
                }
            }
        }
    }

    public class PrintArray
    {
        public PrintArray(char[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++) //GetLength 0 will get rows
            {
                for (int j = 0; j < A.GetLength(1); j++) //GetLength 1 will get columns
                {
                    Console.Write(" {0} ", A[i, j]); // Prints stores cell
                }
                Console.WriteLine();
            }
        }
    }

    public class CheckWalls
    {
        public CheckWalls(char[,] A)
        {

            //A[x, y] = 'X'; // current position in array marked with X
            int paths = 0;
            int choice = 0;

            //-------------------------------------
            MyPosition.x++;
            if (MyPosition.x > -1 && MyPosition.x < 10)
                if (A[MyPosition.x, MyPosition.y] == '0' || A[MyPosition.x, MyPosition.y] == 'E') // down check
                {
                    paths++;
                    choice = 4;
                }
            MyPosition.x--;
            //-------------------------------------
            MyPosition.x--;
            if (MyPosition.x > -1 && MyPosition.x < 10)
                if (A[MyPosition.x, MyPosition.y] == '0' || A[MyPosition.x, MyPosition.y] == 'E') // up check
                {
                    paths++;
                    if (choice < 4)
                        choice = 3;
                }
            MyPosition.x++;
            //-------------------------------------
            MyPosition.y++;
            if (MyPosition.y > -1 && MyPosition.y < 10)
                if (A[MyPosition.x, MyPosition.y] == '0' || A[MyPosition.x, MyPosition.y] == 'E') // right check
                {
                    paths++;
                    if (choice < 3)
                        choice = 2;
                }
            MyPosition.y--;
            //-------------------------------------
            MyPosition.y--;
            if (MyPosition.y > -1 && MyPosition.y < 10)
                if (A[MyPosition.x, MyPosition.y] == '0' || A[MyPosition.x, MyPosition.y] == 'E') // left check
                {
                    paths++;
                    if (choice < 2)
                        choice = 1;
                }
            MyPosition.y++;
            //-------------------------------------


            if (paths > 1) // if more than one path available it will push current location to stack
            {
                MyPosition.pastXStack.Push(MyPosition.x);
                MyPosition.pastYStack.Push(MyPosition.y);

                if (choice == 4)
                    MyPosition.x++;
                if (choice == 3)
                    MyPosition.x--;
                if (choice == 2)
                    MyPosition.y++;
                if (choice == 1)
                    MyPosition.y--;

            }
            if (paths == 1) // if one path available it will move
            {

                if (choice == 4)
                    MyPosition.x++;
                if (choice == 3)
                    MyPosition.x--;
                if (choice == 2)
                    MyPosition.y++;
                if (choice == 1)
                    MyPosition.y--;

            }
            if (paths == 0) // will pop off newest cell and give coorinates where there was a choice in paths
            {
                MyPosition.x = MyPosition.pastXStack.Top();
                MyPosition.y = MyPosition.pastYStack.Top();
            }

            A[MyPosition.x, MyPosition.y] = 'X'; // current position in array marked with X

        }
    }

    public class IntStack
    {
        public int[] theStack; // store the stack
        private int iTop; // iTop will be the top of the stack, the next cell is where item is pushed
        private int next = 0; // keeps track of current position

        public IntStack(int a)
        {
            theStack = new int[a]; // will initiate array with 100 cells
            iTop = -1;
        }

        public void Push(int x)
        {
            if (IsFull())
            {
                Console.WriteLine("Stack is full");
            }
            else
            {
                theStack[next] = x; //stores x from paramete into current cell (next)
                next++; // makes next cell one forward
                iTop++;
            }
        }

        public int Pop()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is Empty");
                return -1;
            }

            next--; // falls back a cell to store one back in stack
            iTop--; // top of stack is back one in stack

            return theStack[next]; // returns new current position to push to
        }

        public int Top()
        {
            return theStack[iTop]; // returns the top of the stack
        }

        public Boolean IsEmpty()
        {
            if (next == -1) // if position next is at beginning of stack
            {
                return true;
            }
            return false;
        }

        public Boolean IsFull()
        {
            if (next == 99) // if position next is at end of stack
            {
                return true;
            }
            return false;
        }
    }

    public class MazeGen
    {
        public char[,] mazeUnderlay = new char[10, 10] {  { '*', '*', '*', 'S', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '0', '*', '0', '*', '*', '*', '0', '*', '*'},
                                                    { '*', '0', '0', '0', '0', '*', '0', '0', '*', '*'},
                                                    { '*', '*', '0', '*', '0', '*', '0', '*', '*', '*'},
                                                    { '*', '0', '0', '*', '0', '*', '0', '0', '0', '*'},
                                                    { '*', '*', '*', '*', '0', '*', '0', '*', '0', '*'},
                                                    { '*', '*', '*', '*', '0', '0', '0', '*', '0', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '0', '*', '0', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '0', '0'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', 'E'}, };

    } // S = Start , E = End , 0 = Hallway , * = Wall
}

