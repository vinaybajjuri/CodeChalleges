using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApplication3
{
    class CalculateShortestPath
    {
        static void Main(string[] args)
        {
            int rows, columns, numberCount = 0;
            Point sourcePoint = new Point();
            Point destinationPoint = new Point();
            Console.WriteLine("Enter rows");
            rows = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter columns");
            columns = Convert.ToInt16(Console.ReadLine());
            int[,] arrayOfNumbers = new int[rows, columns];
            Console.WriteLine("Enter the numbers with 0's and 1's of length" + rows * columns);
            String numbers = Console.ReadLine();
            if (rows < 100 && columns < 100 && rows > 0 && columns > 0 && (rows * columns) == numbers.Length)
            {
                //converting the numbers in to the array and storing in arrayOfNumbers
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        arrayOfNumbers[i, j] = Convert.ToInt16(Convert.ToString(numbers[numberCount]));
                        numberCount++;

                    }
                }
                Console.WriteLine("Enter source point");
                string[] source = Console.ReadLine().Split(',');
                sourcePoint.X = Convert.ToInt16(source[0]);
                sourcePoint.Y = Convert.ToInt16(source[1]);
                Console.WriteLine("Enter destination point");
                string[] destination = Console.ReadLine().Split(',');
                destinationPoint.X = Convert.ToInt16(destination[0]);
                destinationPoint.Y = Convert.ToInt16(destination[1]);
                CalculateShortestPath csp = new CalculateShortestPath();
                if (arrayOfNumbers[sourcePoint.X, sourcePoint.Y] == 1 && arrayOfNumbers[destinationPoint.X, destinationPoint.Y]==1) // it should calcuate the path only if the value in the source point is 1
                    csp.calculateDistance(sourcePoint, destinationPoint, arrayOfNumbers, rows, columns);
                else
                {
                    Console.WriteLine("Source or destination points have value 0. Please try again");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Please enter valid inputs");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public void calculateDistance(Point sourcePoint, Point destinationPoint, int[,] matrix, int rows, int columns)
        {
            Queue<Point> visitedPoints = new Queue<Point>();
            Queue<Point> needToVisit = new Queue<Point>();
            Dictionary<Point, int> intermediatePoints = new Dictionary<Point, int>(); //dictionary because we need to calculate hops
            Point intermediatePoint = new Point();
            visitedPoints.Enqueue(sourcePoint); 
            needToVisit.Enqueue(sourcePoint); //pushing this point at first because we are looping based on the needToVisit Queue 
            intermediatePoints.Add(sourcePoint, 0); //pushing this point in order to calculate the hops using the value part
            while (needToVisit.Count>0)
            {
                sourcePoint = needToVisit.Dequeue(); //As we are enqueuing all the points into this array, we need to make the point in need to visit as source point and continue until it has no values
                if (!(sourcePoint.X == destinationPoint.X && sourcePoint.Y == destinationPoint.Y))
                {
                    if (sourcePoint.X - 1 >= 0)
                    {
                        if (matrix[sourcePoint.X - 1, sourcePoint.Y] == 1)
                        {
                            intermediatePoint.X = sourcePoint.X - 1;
                            intermediatePoint.Y = sourcePoint.Y;

                            if (!visitedPoints.Contains(intermediatePoint)) 
                            {
                                intermediatePoints.Add(intermediatePoint, intermediatePoints[sourcePoint] + 1); //Adding current point and incrementing the value to calculate hops
                                needToVisit.Enqueue(intermediatePoint); //pushing this as elements in the queue will become source points
                                visitedPoints.Enqueue(intermediatePoint);//pusing this to eliminate the points that are already visited
                                if (intermediatePoint == destinationPoint) //break as we got the destination point
                                    break;

                            }
                        }
                    }


                    if (sourcePoint.X + 1 >= 0 && sourcePoint.X +1 < rows)
                    {
                        if (matrix[sourcePoint.X + 1, sourcePoint.Y] == 1)
                        {
                            intermediatePoint.X = sourcePoint.X + 1;
                            intermediatePoint.Y = sourcePoint.Y;
                            if (!visitedPoints.Contains(intermediatePoint))
                            {
                                intermediatePoints.Add(intermediatePoint, intermediatePoints[sourcePoint] + 1); //Adding current point and incrementing the value to calculate hops
                                needToVisit.Enqueue(intermediatePoint); //pushing this as elements in the queue will become source points
                                visitedPoints.Enqueue(intermediatePoint);//pusing this to eliminate the points that are already visited
                                if (intermediatePoint == destinationPoint) //break as we got the destination point
                                  break;
                            }
                        }
                    }

                    if (sourcePoint.Y - 1 >= 0 )
                    {
                        if (matrix[sourcePoint.X, sourcePoint.Y - 1] == 1)
                        {
                            intermediatePoint.X = sourcePoint.X;
                            intermediatePoint.Y = sourcePoint.Y - 1;
                            if (!visitedPoints.Contains(intermediatePoint))
                            {
                                intermediatePoints.Add(intermediatePoint, intermediatePoints[sourcePoint] + 1); //Adding current point and incrementing the value to calculate hops
                                needToVisit.Enqueue(intermediatePoint); //pushing this as elements in the queue will become source points
                                visitedPoints.Enqueue(intermediatePoint);//pusing this to eliminate the points that are already visited
                                if (intermediatePoint == destinationPoint) //break as we got the destination point
                                    break;
                            }
                        }
                    }

                    if (sourcePoint.Y + 1 >= 0 && sourcePoint.Y + 1 < columns)
                    {
                        if (matrix[sourcePoint.X, sourcePoint.Y + 1] == 1)
                        {
                            intermediatePoint.X = sourcePoint.X;
                            intermediatePoint.Y = sourcePoint.Y + 1;
                            if (!visitedPoints.Contains(intermediatePoint)) 
                            {
                                intermediatePoints.Add(intermediatePoint, intermediatePoints[sourcePoint] + 1); //Adding current point and incrementing the value to calculate hops
                                needToVisit.Enqueue(intermediatePoint); //pushing this as elements in the queue will become source points
                                visitedPoints.Enqueue(intermediatePoint);//pusing this to eliminate the points that are already visited
                                if (intermediatePoint == destinationPoint) //break as we got the destination point
                                    break;
                            }
                        }
                    }
                    
                }
            }
            if (intermediatePoints.ContainsKey(destinationPoint)) //checking the destination point in intermediatePoints as we are adding the intermediate points to it
            {
                int shortPath = intermediatePoints[destinationPoint]; //As destination point is available in the array getting the value against that
                Console.WriteLine("Shortest path between source point and destination point is " + shortPath);
                Console.ReadLine();
            }
            else
                Console.WriteLine("No path found between source and destination"); //if destination key is not found in intermediatePoints list.
        }
    }



}

    

