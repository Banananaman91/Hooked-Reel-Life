using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

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
            public float F => G + H;
            

            public Location(Vector3 position, Vector3 destination, Location parent = null)
            {
                Parent = parent;
                Position = position;
                H = Vector3.Distance(position, destination);
            }
        }
        
        public List<Location> adjacentSquares;

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

                adjacentSquares = new List<Location>()
                {
                    new Location(currentPosition.Position + Vector3.forward, targetPosition, currentPosition),
                    new Location(currentPosition.Position + Vector3.back, targetPosition, currentPosition),
                    new Location(currentPosition.Position + Vector3.left, targetPosition, currentPosition),
                    new Location(currentPosition.Position + Vector3.right, targetPosition, currentPosition)
                };
                foreach (var aSquare in adjacentSquares)
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
            Debug.Log(openList);
        }
    }
}