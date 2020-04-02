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
            RunThis();
        }

        #region RUN

        static void RunThis()
        {
            MazeGen mapOne = new MazeGen();
            SolveMaze SolveOne = new SolveMaze(mapOne.mazeUnderlay);

            Console.ReadLine();

        }

        // print array
        public static void PrintArray(char[,] A)
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

        #endregion

    }

    class SolveMaze
    {
        IntStack xChoiceStack = new IntStack(100); // store x position when a choice appears on maze
        IntStack yChoiceStack = new IntStack(100); // store y position when a choice appears on maze
        IntStack pastXStack = new IntStack(100);
        IntStack pastYStack = new IntStack(100);

        private char currentChar = '*';
        private int x;
        private int y;
        private int xEnd;
        private int yEnd;

        public SolveMaze(char[,] A) // solve a character array
        {
            findEnd(A); // finds ending point
            findStart(A); //finds starting point

            while (x != xEnd && y != yEnd) // keep going until x and y are the same as end x and y values
            {
                checkWalls1(A);
                Console.Clear();
                PrintArray(A);
                Thread.Sleep(10000);
            }
            Console.ReadLine();


        }

        public static void PrintArray(char[,] A)
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

        private void checkWalls1(char[,] A)
        {

            if (A[x++, y] == '0')
            {
                x++;
                goto Right;
            }
                else x--;

            if (A[x--, y] == '0')
            {
                x--;
                goto Left;
            }
            else x++;

            if (A[x, y++] == '0')
            {
                y++;
                goto Down;
            }
            else y--;

            if (A[x, y--] == '0')
            {
                y--;
                goto Up;
            }
            else y++;

        Down:
                A[x, y] = 'X'; // change current x y position to an x

        Left:
                A[x, y] = 'X'; // change current x y position to an x

        Up:
                A[x, y] = 'X'; // change current x y position to an x

        Right:
                A[x, y] = 'X'; // change current x y position to an x

        }

        private void checkWalls(char[,] A)
        {

            xChoiceStack.Push(x);
            yChoiceStack.Push(y);
            A[x, y] = 'X';
            int currentX = x;
            int currentY = y;

            if(currentX+1 < 10 && currentX+1 > -1)
            if (A[x+1, y] == '0' || A[x+1, y] == 'X')
            {
                if (A[x++, y] == 'X')
                {
                    pastXStack.Push(x);
                    pastYStack.Push(y);
                    A[x++, y] = 'B';
                }
                else
                {
                    x++;
                }
            }

            if(currentX-1 < 10 && currentX-1 > -1)
            if (A[x--, y] == '0' || A[x--, y] == 'X')
            {
                if (A[x--, y] == 'X')
                {
                    pastXStack.Push(x);
                    pastYStack.Push(y);
                    A[x--, y] = 'B';
                }
                else
                {
                    x--;
                }
            }

            if( currentY+1 < 10 && currentY+1 > -1)
            if (A[x, y++] == '0' || A[x, y++] == 'X')
            {
                if (A[x, y++] == 'X')
                {
                    pastXStack.Push(x);
                    pastYStack.Push(y);
                    A[x, y++] = 'B';
                }
                else
                {
                    y++;
                }
            }

            if( currentY-1 < 10 && currentY-1 > -1)
            if (A[x, y--] == '0' || A[x, y--] == 'X')
            {
                if (A[x, y--] == 'X')
                {
                    pastXStack.Push(x);
                    pastYStack.Push(y);
                    A[x, y--] = 'B';
                }
                else
                {
                    y--;
                }
            }
        }
        private void findStart(char[,] A) // searches for S in array and stores x and y
        {
            for (int i = 0; i < A.GetLength(0); i++) //GetLength 0 will get rows
            {
                for (int j = 0; j < A.GetLength(1); j++) //GetLength 1 will get columns
                {
                    if (A[i, j] == 'S')
                    {
                        x = i;
                        y = j;
                        break;
                    }
                }
            }
        }  
        private void findEnd(char[,] A) // searches for E in array and stores xEnd and yEnd
        {
            for (int i = 0; i < A.GetLength(0); i++) //GetLength 0 will get rows
            {
                for (int j = 0; j < A.GetLength(1); j++) //GetLength 1 will get columns
                {
                    if (A[i, j] == 'E')
                    {
                        xEnd = i;
                        yEnd = j;
                        break;
                    }
                }
            }
        }
    }

    class IntStack
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

    class CharStack
    {
        private char[] theStack; // store the stack
        private int iTop; // iTop will be the top of the stack, the next cell is where item is pushed
        private int next = 0; // keeps track of current position

        public CharStack()
        {
            theStack = new char[100]; // will initiate array with 100 cells
            iTop = -1;
        }

        public void Push(char x)
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

        public string Combine(char[] A)
        {
            string combinedSt = new string(A); // will create a new string with characters from A char array
            return combinedSt;
        }

        public static bool IsPalindrome(string s)
        {
            int min = 0; // minimum length of string
            int max = s.Length - 1; // maximum length of string -1 to only go to last letter

            while (true) //keeps going until false is returned
            {
                if (min > max) // will keep true until out of letters when min passes max
                {
                    return true; // if it makes it this far it is palindrom will return true
                }

                char a = s[min];// first letter in string
                char b = s[max];// last letter in string

                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false; // will return false if 
                }
                min++; // add one to min for next letter
                max--; // remove one for next letter
            }
        }
    }

    class IntQueue
    {
        public int[] theQueue;
        // head will store index top of Queue
        private int head;
        // rear will store index to add to
        private int rear = 0;

        public IntQueue()
        {
            theQueue = new int[6];
            head = -1; // top of queue
        }

        public void Reset()
        {
            head = -1;
            rear = 0;
        }

        // method to add int to queue
        public void enqueue(int e)
        {
            if (isFull())
            {
                // at beginning of queue if head is 99 this will return nothing
                // will not have a space to dequeue from
                Console.WriteLine("Queue is full");
                return;
            }

            // store 'e' in index 'rear' 
            theQueue[rear] = e;
            // add 1 to rear to have next index in Q ready for next enqueue call
            rear++;

        }

        public int dequeue()
        {
            // isEmpty set to true value by default
            // finds if queue is empty from beginning before running actual code
            if (isEmpty())
            {
                Console.WriteLine("Queue is empty");
                return -1;
            }

            int result;
            head++;
            // will return number that is pulled out of queue from 'head' index
            result = theQueue[head];
            // replaces pulled number with 0
            theQueue[head] = 0;
            // will add to 'head' counter new index will be latest current number

            return result;

        }

        public Boolean isEmpty()
        {
            // if head is either at -1 then the queue has been emptied
            // those are the two ends of the queue
            if (head + 1 == rear)
            {
                return true;
            }
            return false;
        }

        public Boolean isFull()
        {
            if (rear == 99) // if the rear of queue is reached will return true
            {
                return true;
            }
            return false;
        }

    }

    class MazeGen
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

        public char[,] mazeOverlay = new char[10, 10] {  { '*', '*', '*', 'S', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'},
                                                    { '*', '*', '*', '*', '*', '*', '*', '*', '*', 'E'}, };

        public MazeGen()
        {

        }

    }

    class PlayerMoves
    {
        // points in array of start and exit
        private int startX = 4;
        private int startY = 0;
        private int exitX = 10;
        private int exitY = 10;
        // current player position and 
        private int currentX = 4;
        private int currentY = 0;
        private char currentChar = '*';

        public PlayerMoves()
        {

        }
    }
}

