using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : MonoBehaviour
    {
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

        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);

            var openList = new List<Location>();
            var closedList = new List<Location>();
            var adjacentSquares = new List<Location>();

            Location squareWithLowestFScore = startLocation;

            Debug.Log("Started FindPath");
            Debug.Log("Start location: " + startLocation.PositionInWorld);
            Debug.Log("Target pos: " + targetPosition);

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
                    Debug.Log("PATH FOUND!");
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
            }

            Debug.Log("OPEN LIST:");
            foreach (var space in openList)
            {
                Debug.Log(space.PositionInWorld);
            }

            Debug.Log("CLOSED LIST:");
            foreach (var closedSpace in closedList)
            {
                Debug.Log(closedSpace.PositionInWorld);
            }

            Debug.Log("Finished FindPath");


            // Creating Path List:

            Debug.Log("START PATH LIST");

            List<Vector3> pathToFollow = new List<Vector3>();

            pathToFollow.Add(closedList.Last().PositionInWorld);

            pathToFollow.Add(closedList.Last().Parent.PositionInWorld);
            closedList.Remove(closedList.Last());

            //while(pathToFollow.Any(x => x != startPosition))
            //{
            //    foreach (Location locationToCheck in closedList.ToArray())
            //    {
            //        if(locationToCheck.Parent != null)
            //        {
            //            pathToFollow.Add(closedList.Last().Parent.PositionInWorld);
            //            closedList.Remove(closedList.Last());
            //        }
            //    }
            //}



            //while (pathToFollow.Any(x => x != targetPosition)) // While the pathToFollow list doesn't contain the targetPosition
            //{
            //    foreach (Vector3 pathVector in pathToFollow)
            //    {
            //        Debug.Log(pathVector);
            //    }

            //    foreach (Location locationToCheck in closedList) // Check each location in closedList
            //    {
            //        //Debug.Log("locationToCheck: " + locationToCheck.PositionInWorld);
            //        //Debug.Log("locationToCheck Parent: " + locationToCheck.Parent.PositionInWorld);

            //        if (locationToCheck.Parent != null)
            //        {
            //            if (locationToCheck.Parent.PositionInWorld == pathToFollow.Last())  // If the parent of the current location
            //                                                                                // was the last added to pathToFollow
            //            {
            //                pathToFollow.Add(locationToCheck.PositionInWorld); // Add current to pathToFollow
            //            }
            //        }
            //    }
            //}

            Debug.Log("PATH TO FOLLOW:");
            foreach (Vector3 pathVector in pathToFollow)
            {
                Debug.Log(pathVector);
            }

            yield return null;
        }
    }
}