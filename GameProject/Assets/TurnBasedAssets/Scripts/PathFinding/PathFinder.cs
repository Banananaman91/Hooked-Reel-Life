using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : MonoBehaviour
    {
        public class Location
        {
            //public Location Parent { get; set; }
            //public Vector3 Position { get; }

            public Location Parent;
            public Vector3 Position;

            public float G => Parent?.G + 1 ?? 0;
            public float H;
            public float F => Mathf.Round(G + H);
            

            public Location(Vector3 position, Vector3 destination, Location parent)
            {
                Parent = parent;
                Position = position;
                H = Vector3.Distance(position, destination);
            }
        }
        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);
            //Location targetLocation = null;

            var openList = new List<Location>();
            var closedList = new List<Location>();
            var adjacentSquares = new List<Location>();

            Location squareWithLowestFScore = startLocation;
            //Location previousSquare = null;

            Debug.Log("Started FindPath");
            Debug.Log("Start location: " + startLocation.Position);
            Debug.Log("Target pos: " + targetPosition);

            openList.Add(startLocation);

            while (openList.Count > 0)
            {
                currentLocation = squareWithLowestFScore;

                closedList.Add(currentLocation);
                openList.Remove(currentLocation);

                if (closedList.Any(x => x.Position == targetPosition))
                {
                    Debug.Log("PATH FOUND!");
                    break;
                }
                
                adjacentSquares.Clear();
                adjacentSquares.Add(new Location(currentLocation.Position + Vector3.forward, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.Position + Vector3.back, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.Position + Vector3.left, targetPosition, currentLocation));
                adjacentSquares.Add(new Location(currentLocation.Position + Vector3.right, targetPosition, currentLocation));


                foreach (var adjacentSquare in adjacentSquares)
                {
                    Debug.Log(adjacentSquare.F);

                    if (closedList.Any(x => x.Position == adjacentSquare.Position)) // If the adjacentSqaure is in the closedList
                    {
                        continue;
                    }

                    if (openList.All(x => x.Position != adjacentSquare.Position)) // If the adjacentSquare is not in the openList
                    {
                        openList.Add(adjacentSquare);
                    }

                    if (adjacentSquare.F < openList.First(x => x.Position == adjacentSquare.Position).F)
                    {
                        squareWithLowestFScore = adjacentSquare;
                    }
                }

                yield return null;
            }

            yield return null;

            foreach (var space in openList)
            {
                Debug.Log(space.Position);
            }

            foreach (var closedSpace in closedList)
            {
                Debug.Log(closedSpace.Position);
            }

            Debug.Log("Finished FindPath");
        }
    }
}