using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGame
{
    public class GameEngine
    {
        private bool[,] Area;
        private readonly int rows;
        private readonly int cols;
        Random rand = new Random();

        int[] numsAlive = new int[0];
        int[] numsDead = new int[0];

        public GameEngine(int rows, int cols, bool isStruct, int key, string alive, string dead)
        {
            this.rows = rows;
            this.cols = cols;
            Area = new bool[cols, rows];

            int[] temp = new int[2] { 0, 0 };
            for (int i = 0; i < 9; i++)
            {
                if (alive[i] == '1')
                {
                    Array.Resize(ref numsAlive, numsAlive.Length + 1);
                    numsAlive[temp[0]] = i;
                    temp[0]++;
                }
                if (dead[i] == '1')
                {
                    Array.Resize(ref numsDead, numsDead.Length + 1);
                    numsDead[temp[1]] = i;
                    temp[1]++;
                }
            }

            if (!isStruct) GenerateRandomArea(key);
            else GenerateStruct(key);
        }

        public string GetNumbersData()
        {
            string output = "";
            for (int i = 0; i < numsAlive.Length; i++) output += numsAlive[i].ToString()+" ";
            output += "\n";
            for (int i = 0; i < numsDead.Length; i++) output += numsDead[i].ToString()+" ";
            return output;
        }

        public bool[,] GetCurrentGeneration()
        {
            bool[,] tempArea = new bool[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    tempArea[i, j] = Area[i, j];
                }
            }

            return tempArea;
        }

        public void NextGeneration()
        {
            bool[,] newArea = new bool[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int countFriends = GetCountFriends(i, j);
                    bool isLive = Area[i, j];

                    if (!isLive && Array.IndexOf(numsDead, countFriends) != -1) newArea[i, j] = true;
                    else if (isLive && Array.IndexOf(numsAlive, countFriends) == -1) newArea[i, j] = false;
                    else newArea[i, j] = isLive;
                }
            }
            Area = newArea;
        }

        private int GetCountFriends(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isMe = i == 0 && j == 0;
                    bool isLive = Area[col, row];

                    if (!isMe && isLive) count++;
                }
            }
            return count;
        }

        private void GenerateRandomArea(int density)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Area[i, j] = rand.Next(density) == 0;
                }
            }
        }

        private void GenerateStruct(int numberStruct)
        {
            int x = cols / 2;
            int y = rows / 2;

            switch (numberStruct)
            {
                case 1:
                    {
                        Area[x, y + 1] = true;
                        Area[x + 1, y] = true;
                        Area[x + 1, y - 1] = true;
                        Area[x, y - 1] = true;
                        Area[x - 1, y - 1] = true;
                    }
                    break;

                case 2:
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            Area[x, i] = true;
                        }
                    }
                    break;

                case 3:
                    {
                        for (int i = 0; i < cols; i++)
                        {
                            Area[i, y] = true;
                        }
                    }
                    break;

                case 4:
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            Area[x, i] = true;
                        }
                        for (int i = 0; i < cols; i++)
                        {
                            Area[i, y] = true;
                        }
                    }
                    break;

                case 5:
                    {
                        Area[x, y] = true;
                        Area[x - 1, y] = true;
                        Area[x + 1, y] = true;
                        Area[x, y + 1] = true;
                    }
                    break;

                case 6:
                    {
                        Area[x, y] = true;
                        Area[x - 1, y] = true;
                        Area[x, y + 1] = true;
                        Area[x - 1, y + 1] = true;
                        Area[x + 1, y + 1] = true;
                        Area[x - 2, y] = true;
                    }
                    break;

                case 7:
                    {
                        Area[x - 1, y] = true;
                        Area[x, y + 1] = true;
                        Area[x, y + 2] = true;
                        Area[x + 1, y] = true;
                        Area[x + 1, y - 1] = true;
                        Area[x + 2, y + 1] = true;
                    }
                    break;

                case 8:
                    {
                        Area[x - 1, y + 2] = true;
                        Area[x, y + 2] = true;
                        Area[x - 1, y + 1] = true;
                        Area[x, y + 1] = true;
                        Area[x + 1, y] = true;
                        Area[x + 1, y - 1] = true;
                        Area[x + 2, y] = true;
                        Area[x + 2, y - 1] = true;
                    }
                    break;

                case 9:
                    {
                        Area[x, y + 6] = true;
                        Area[x + 1, y + 6] = true;
                        Area[x, y + 7] = true;
                        Area[x + 1, y + 7] = true;
                        Area[x + 4, y] = true;
                        Area[x + 5, y] = true;
                        Area[x + 4, y + 1] = true;
                        Area[x + 5, y + 1] = true;
                        Area[x + 6, y + 10] = true;
                        Area[x + 7, y + 10] = true;
                        Area[x + 6, y + 11] = true;
                        Area[x + 7, y + 11] = true;
                        Area[x + 10, y + 4] = true;
                        Area[x + 10, y + 5] = true;
                        Area[x + 11, y + 4] = true;
                        Area[x + 11, y + 5] = true;

                        Area[x + 3, y + 4] = true;
                        Area[x + 3, y + 5] = true;
                        Area[x + 3, y + 6] = true;
                        Area[x + 3, y + 7] = true;
                        Area[x + 8, y + 4] = true;
                        Area[x + 8, y + 5] = true;
                        Area[x + 8, y + 6] = true;
                        Area[x + 8, y + 7] = true;
                        Area[x + 4, y + 3] = true;
                        Area[x + 5, y + 3] = true;
                        Area[x + 6, y + 3] = true;
                        Area[x + 7, y + 3] = true;
                        Area[x + 4, y + 8] = true;
                        Area[x + 5, y + 8] = true;
                        Area[x + 6, y + 8] = true;
                        Area[x + 7, y + 8] = true;

                        Area[x + 4, y + 6] = true;
                        Area[x + 5, y + 4] = true;
                        Area[x + 6, y + 5] = true;
                    }
                    break;

                case 10:
                    {
                        Area[x - 3, y] = true;
                        Area[x - 2, y] = true;
                        Area[x - 1, y] = true;
                        Area[x + 1, y] = true;
                        Area[x + 2, y] = true;
                        Area[x + 3, y] = true;
                        Area[x - 3, y - 1] = true;
                        Area[x + 3, y - 1] = true;
                        Area[x - 1, y - 2] = true;
                        Area[x + 1, y - 2] = true;
                        Area[x - 1, y - 3] = true;
                        Area[x - 2, y - 3] = true;
                        Area[x + 1, y - 3] = true;
                        Area[x + 2, y - 3] = true;
                        Area[x + 3, y - 4] = true;
                        Area[x - 3, y - 4] = true;
                    }
                    break;

                default:
                    {
                        Area[x - 1, y] = true;
                        Area[x, y] = true;
                        Area[x + 1, y] = true;
                    }
                    break;
            }
        }
    }
}
