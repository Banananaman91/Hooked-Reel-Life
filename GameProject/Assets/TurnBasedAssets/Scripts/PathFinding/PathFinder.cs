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
            public Location Parent { get; set; }
            public Vector3 Position { get; }
            public float G => Parent?.G + 1 ?? 0;
            public float H;
            public float F => Mathf.Round(G + H);
            

            public Location(Vector3 position, Vector3 destination, Location parent = null)
            {
                Parent = parent;
                Position = position;
                H = Vector3.Distance(position, destination);
            }
        }
        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Location current;
            Location start = new Location(startPosition, targetPosition);
            Location target = null;
            var openList = new List<Location>();
            var closedList = new List<Location>();
            var adjacentSquares = new List<Location>();
            Location squareWithLowestFScore = start;
            Location previousSquare = null;
            Debug.Log("FindPath");
            openList.Add(start);

            while (openList.Count > 0)
            {
                current = squareWithLowestFScore;

                closedList.Add(current);
                openList.Remove(current);

                if (closedList.Any(x => x.Position == targetPosition))
                {
                    Debug.Log("PathFound!");
                    break;
                }
                
                adjacentSquares.Clear();
                adjacentSquares.Add(new Location(current.Position + Vector3.forward, targetPosition, current));
                adjacentSquares.Add(new Location(current.Position + Vector3.back, targetPosition, current));
                adjacentSquares.Add(new Location(current.Position + Vector3.left, targetPosition, current));
                adjacentSquares.Add(new Location(current.Position + Vector3.right, targetPosition, current));


                foreach (var aSquare in adjacentSquares)
                {
                    if (closedList.Any(x => x.Position == aSquare.Position))
                    {
                        continue;
                    }

                    if (openList.All(x => x.Position != aSquare.Position))
                    {
                        openList.Add(aSquare);
                    }

                    if (aSquare.F < openList.First(x => x.Position == aSquare.Position).F)
                    {
                        squareWithLowestFScore = aSquare;
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
        }
    }
}