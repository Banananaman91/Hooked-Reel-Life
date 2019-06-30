using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : MonoBehaviour
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

        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, Action<IEnumerable<Vector3>> onCompletion)
        {
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);

            var openList = new List<Location>();
            var closedList = new List<Location>();
            var adjacentSquares = new List<Location>();

            Location squareWithLowestFScore = startLocation;

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
                adjacentSquares.Add(new Location(currentLocation.PositionInWorld + Vector3.forward, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.PositionInWorld + Vector3.back, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.PositionInWorld + Vector3.left, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.PositionInWorld + Vector3.right, targetPosition, currentLocation));


                foreach (var adjacentSquare in adjacentSquares)
                {
                    if (closedList.Any(x => x.PositionInWorld == adjacentSquare.PositionInWorld)) // If the adjacentSqaure is in the closedList
                    {
                        break;
                    }

                    if (openList.All(x => x.PositionInWorld != adjacentSquare.PositionInWorld)) // If the adjacentSquare is not in the openList
                    {
                        openList.Add(adjacentSquare);
                    }

                    if (adjacentSquare.F < openList.First(x => x.PositionInWorld == adjacentSquare.PositionInWorld).F)
                    {
                        squareWithLowestFScore = adjacentSquare;
                    }
                }

                yield return null;
            }

            pathToFollow.Clear();

            pathToFollow.Add(closedList.Last().PositionInWorld);

            do
            {
                pathToFollow.Add(closedList.Last().Parent.PositionInWorld);
                closedList.Remove(closedList.Last());
            } while (!pathToFollow.Contains(startPosition));

            pathToFollow.Reverse();

            onCompletion(pathToFollow);
        }
    }
}