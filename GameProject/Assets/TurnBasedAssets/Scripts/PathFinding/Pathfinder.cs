using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TurnBasedAssets.Scripts.Pathfinding
{
    public class Pathfinder : IPathfinder, IObjectAvoidanceInitialisable
    {
        private List<Vector3> _pathToFollow = new List<Vector3>();
        private float _2dMaxDistance = 1;
        private ObjectAvoidance _avoidance;
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

        public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, bool is3d, float movementRadius, Action<IEnumerable<Vector3>> onCompletion)
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new ObjectRequestMessage(this));
            List<Location> openList = new List<Location>();
            List<Location> closedList = new List<Location>();
            Location currentLocation;
            Location startLocation = new Location(startPosition, targetPosition, null);
            var isObjectMoving = _avoidance.Objects.First(x => x.transform.position == startPosition);

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
                adjacentSquares = !is3d ? GetAdjacentSquares2D(currentLocation, targetPosition, isObjectMoving) : GetAdjacentSquares3D(currentLocation, targetPosition, isObjectMoving);

                foreach (var adjacentSquare in adjacentSquares)
                {
                    if (closedList.Any(x => x.PositionInWorld == adjacentSquare.PositionInWorld)) continue;

                    if (openList.All(x => x.PositionInWorld != adjacentSquare.PositionInWorld)) openList.Add(adjacentSquare);

                    else if (adjacentSquare.F < openList.First(x => x.PositionInWorld == adjacentSquare.PositionInWorld).F) openList.First(x => x.PositionInWorld == adjacentSquare.PositionInWorld).Parent = adjacentSquare;
                }

                yield return null;
            }

            _pathToFollow.Clear();

            var current = closedList.Last();

            _pathToFollow.Add(current.PositionInWorld);
            
            do
            {
                _pathToFollow.Add(current.Parent.PositionInWorld);
                current = current.Parent;
            } while (!_pathToFollow.Contains(startPosition));

            _pathToFollow.Reverse();

            onCompletion(_pathToFollow);
        }

        private List<Location> GetAdjacentSquares2D(Location point, Vector3 target, Controller isObjectMoving)
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

                    bool isIntersecting = _avoidance.Objects
                        .Where(x => x!= isObjectMoving && Vector3.Distance(x.transform.position, adjacentVector.PositionInWorld) <=
                                    Vector3.Distance(point.PositionInWorld, target)).Any(x => x.RenderBounds.bounds.Contains(adjacentVector.PositionInWorld));
                    if (!isIntersecting) returnList.Add(adjacentVector);
                }
            }
            return returnList;
        }
        
        private List<Location> GetAdjacentSquares3D(Location point, Vector3 target, Controller isObjectMoving)
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
                        bool isIntersecting = _avoidance.Objects
                            .Where(x => x!= isObjectMoving && Vector3.Distance(x.transform.position, adjacentVector.PositionInWorld) <=
                                        Vector3.Distance(point.PositionInWorld, target)).Any(x => x.RenderBounds.bounds.Contains(adjacentVector.PositionInWorld));
                        if (!isIntersecting) returnList.Add(adjacentVector);
                    }
                }
            }

            return returnList;
        }
        
        public void ObjectInitialise(ObjectAvoidance objectAvoidance)
        {
            _avoidance = objectAvoidance;
        }
    }
}