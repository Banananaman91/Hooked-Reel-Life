using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedAssets.Scripts.Interface;
using UnityEngine;
using UnityEngine.UIElements;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinder : IPathFinder
    {
        public List<Vector3> pathToFollow = new List<Vector3>();

        private float _2dMaxDistance = 1;
        public class Location
        {
            public Location Parent { get; set; }
            public Vector3 PositionInWorld { get; }

            private float G => Parent?.G + 1 ?? 0;
            private float H { get; }
            public float F => Mathf.Round(G + H);


            public Location(Vector3 position, Vector3 destination, Location parent)
            {
                Parent = parent;
                PositionInWorld = position;
                H = Vector3.Distance(position, destination);
            }
        }

        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, bool is3D, float movementRadius, Action<IEnumerable<Vector3>> onCompletion)
        {
            List<Location> openList = new List<Location>();
            List<Location> closedList = new List<Location>();
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);
            
            Collider[] hitColliders = Physics.OverlapSphere(startPosition, movementRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                var objectPosition = hitColliders[i].transform.position;
                var objectScaleX = hitColliders[i].transform.localScale.x;
                var objectScaleY = hitColliders[i].transform.localScale.y;
                var objectScaleZ = hitColliders[i].transform.localScale.z;
                Location newLocation = new Location(objectPosition, targetPosition, null);
                for (float xIndex = objectPosition.x - objectScaleX; xIndex <= objectPosition.x + objectScaleX; xIndex++)
                {
                    for (float zIndex = objectPosition.z - objectScaleZ; zIndex <= objectPosition.z + objectScaleZ; zIndex++)
                    {
                        for (float yIndex = objectPosition.y - objectScaleY; yIndex <= objectPosition.y + objectScaleY; yIndex++)
                        {
                            if (hitColliders[i].bounds.Contains(new Vector3(xIndex, yIndex, zIndex)))
                            {
                                Location adjacentLocation = new Location(new Vector3(xIndex, yIndex, zIndex), targetPosition, newLocation);
                                closedList.Add(adjacentLocation);
                                if (closedList.Any(x => x.PositionInWorld == targetPosition))
                                {
                                    closedList.Remove(adjacentLocation);
                                }
                            }
                        }
                    }
                }
                if (closedList.Any(x => x.PositionInWorld == targetPosition))
                {
                    closedList.Remove(newLocation);
                }
            }

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
                    var adjacentVector =
                        new Location(new Vector3(xIndex, point.PositionInWorld.y, zIndex), target, point);
                    if (Vector3.Distance(point.PositionInWorld, adjacentVector.PositionInWorld) >
                        _2dMaxDistance) continue;
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