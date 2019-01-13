using System;
using System.Linq;
using System.Collections.Generic;

namespace ManaMist.Utility
{
    public class Pathfinding
    {
        public Coordinate start;
        public int maxDistance;

        public Dictionary<Coordinate, Path> Search(Func<Coordinate, bool> canMoveFunction)
        {
            Dictionary<Coordinate, Path> possiblePaths = new Dictionary<Coordinate, Path>();
            List<Path> closedPaths = new List<Path>();
            Queue<Path> queue = new Queue<Path>();
            Path path = new Path() {
                start
            };
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                Path currentPath = queue.Dequeue();
                closedPaths.Add(currentPath);
                Coordinate coordinate = currentPath.Last();

                // If path doesn't exist or is smaller than stored value, update path
                if (!possiblePaths.ContainsKey(coordinate) || possiblePaths[coordinate]?.Count > currentPath.Count)
                {
                    possiblePaths.Add(currentPath.Last(), currentPath);
                }

                // Enqueue paths that aren't closed
                foreach (Coordinate neighbor in coordinate.GetNeighbors())
                {
                    Path nextPath = new Path(currentPath);
                    nextPath.Add(neighbor);
                    if (!closedPaths.Contains(nextPath) && nextPath.Count <= maxDistance && canMoveFunction(coordinate))
                    {
                        queue.Enqueue(nextPath);
                    }
                }
            }

            return possiblePaths;
        }
    }

    public class Path : List<Coordinate>
    {
        public Path() : base() { }
        public Path(List<Coordinate> oldPath) : base(oldPath)
        {
        }
    }
}