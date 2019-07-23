using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedAssets.Scripts.Interface;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : IPathFinder
    {
        public List<Vector3> pathToFollow = new List<Vector3>();
        public class Location
        {
            public Location Parent { get; set; }
            public Vector3 PositionInWorld { get; }

            public float G => Parent?.G + 1 ?? 0;
            public float H;
            public float F => Mathf.Round(G + H);


            public Location(Vector3 position, Vector3 destination, Location parent)
            {
                Parent = parent;
                PositionInWorld = position;
                H = Vector3.Distance(position, destination);
            }
        }

        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, bool is3D, Action<IEnumerable<Vector3>> onCompletion)
        {
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);
            var openList = new List<Location>();
            var closedList = new List<Location>();
            var adjacentSquares = new List<Location>();
            openList.Add(startLocation);

            while (openList.Count > 0)
            {
                // Find square with lowest F value
                var lowestFScore = openList.Min(x => x.F);
                currentLocation = openList.First(x => x.F == lowestFScore);

                closedList.Add(currentLocation);
                openList.Remove(currentLocation);

                if (closedList.Any(x => x.PositionInWorld == targetPosition))
                {
                    break;
                }

                adjacentSquares.Clear();
                adjacentSquares = !is3D ? GetAdjacentSquares2D(currentLocation, targetPosition) : GetAdjacentSquares3D(currentLocation, targetPosition);

                foreach (var adjacentSquare in adjacentSquares)
                {
                    if (closedList.Any(x => x.PositionInWorld == adjacentSquare.PositionInWorld)) // If the adjacentSqaure is in the closedList
                    {
                        continue;
                    }

                    if (openList.All(x => x.PositionInWorld != adjacentSquare.PositionInWorld)) // If the adjacentSquare is not in the openList
                    {
                        openList.Add(adjacentSquare);
                    }

                    else if (adjacentSquare.F < openList.First(x => x.PositionInWorld == adjacentSquare.PositionInWorld).F)
                    {
                        openList.First(x => x.PositionInWorld == adjacentSquare.PositionInWorld).Parent =
                            adjacentSquare;
                    }
                }

                yield return null;
            }

            pathToFollow.Clear();

            var current = closedList.Last();

            pathToFollow.Add(current.PositionInWorld);
            
            do
            {
                pathToFollow.Add(current.Parent.PositionInWorld);
                current = current.Parent;
            } while (!pathToFollow.Contains(startPosition));

            pathToFollow.Reverse();

            onCompletion(pathToFollow);
        }

        private List<Location> GetAdjacentSquares2D(Location point, Vector3 target)
        {
            List<Location> returnList = new List<Location>();

            for (float xIndex = point.PositionInWorld.x - 1; xIndex <= point.PositionInWorld.x + 1; xIndex++)
            {
                for (float zIndex = point.PositionInWorld.z - 1; zIndex <= point.PositionInWorld.z + 1; zIndex++)
                {
                    var adjacentVector = new Location(new Vector3(xIndex, point.PositionInWorld.y, zIndex),target, point);
                    if (Vector3.Distance(point.PositionInWorld, adjacentVector.PositionInWorld) > 1) continue;
                    returnList.Add(adjacentVector);
                }
            }

            return returnList;
        }
        
        private List<Location> GetAdjacentSquares3D(Location point, Vector3 target)
        {
            List<Location> returnList = new List<Location>();

            for (float xIndex = point.PositionInWorld.x - 1; xIndex <= point.PositionInWorld.x + 1; xIndex++)
            {
                for (float yIndex = point.PositionInWorld.y - 1; yIndex <= point.PositionInWorld.y + 1; yIndex++)
                {
                    for (float zIndex = point.PositionInWorld.z - 1; zIndex <= point.PositionInWorld.z + 1; zIndex++)
                    {
                        var adjacentVector = new Location(new Vector3(xIndex, yIndex, zIndex), target,
                            point);
                        returnList.Add(adjacentVector);
                    }
                }
            }

            return returnList;
        }
    }
}