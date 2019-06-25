using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : MonoBehaviour
    {
        class Location
        {
            public Location Parent { get; set; }
            public Vector3 Position { get; }
            public float G => Parent?.G + 1 ?? 0;
            public float H;
            public float F => G + H;
            public List<Location> adjacentSquares;

            public Location(Vector3 position, Vector3 destination, Location parent = null)
            {
                Parent = parent;
                Position = position;
                H = Vector3.Distance(position, destination);
                adjacentSquares = new List<Location>()
                {
                    new Location(Position + Vector3.forward, destination, this),
                    new Location(Position + Vector3.back, destination, this),
                    new Location(Position + Vector3.left, destination, this),
                    new Location(Position + Vector3.right, destination, this)
                };
            }
        }

        public void FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Location current = null;
            var start = new Location(startPosition, targetPosition);
            var target = new Location(targetPosition, startPosition);
            var openList = new List<Location>();
            var closedList = new List<Location>();
            var squareWithLowestFScore = start;


            openList.Add(start);
            while (openList.Count > 0)
            {
                var currentPosition = squareWithLowestFScore;

                closedList.Add(currentPosition);
                openList.Remove(currentPosition);

                if (closedList.Contains(target))
                {
                    Debug.Log("PathFound!");
                    break;
                }


                foreach (var aSquare in current.adjacentSquares)
                {
                    if (closedList.Contains(aSquare))
                    {
                        continue;
                    }

                    if (!openList.Contains(aSquare))
                    {
                        openList.Add(aSquare);
                    }

                    else if (aSquare.F < squareWithLowestFScore.F)
                    {
                        squareWithLowestFScore = aSquare;
                    }
                }
            }
        }
    }
}