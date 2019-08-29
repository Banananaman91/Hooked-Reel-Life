using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerControls
{
    public class PlayerController : Controller
    {
        private IEnumerable<Vector3> path = new List<Vector3>();
        private List<GameObject> pathVisualized = new List<GameObject>();
        private Vector3 previousLocation;
        private Vector3 previousDistance;

        private float SphereRadius => mouseSelectionScript.MoveableRadius;

        private void Start()
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PathFinderRequestMessage(this));
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
            SetPosition();
        }

        private void SetPosition()
        {
            transform.position = Position.Reposition(transform.position, mouseSelectionScript.PlanePosition);
        }

        public void Initialise(IPathfinder pathfinder)
        {
            Pathfinder = pathfinder;
        }

        public override void Initialise(IPosition iPosition)
        {
            Position = iPosition;
        }

        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            ClearTiles();
            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, rawGridPoint, false, SphereRadius,newPath => path = newPath));
            foreach (var location in path)
            {
                if (location == rawGridPoint) break;
                var tile = Instantiate(pathFinderTiles, location, Quaternion.identity);
                pathVisualized.Add(tile);
            }
            yield return null;
        }

        public IEnumerator StartPlayerMovement()
        {
            mouseSelectionScript.enabled = false;
            foreach (var location in path)
            {
                Vector3 locationDistance = location - previousLocation;
                if (locationDistance != previousDistance)
                {
                    yield return StartCoroutine(RotatePlayer(location));
                }

                while (transform.position != location)
                {
                    yield return StartCoroutine(MoveToNextTile(location));
                }

                previousDistance = location - previousLocation;
                previousLocation = location;

            }

            mouseSelectionScript.enabled = true;
            ClearTiles();
            mouseSelectionScript.Selection.DeSelect();
            yield return null;
            
        }
        
        private IEnumerator MoveToNextTile(Vector3 location)
        {
            transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
            yield return null;
        }
        
        private IEnumerator RotatePlayer(Vector3 location)
        {
            Vector3 targetDir = location - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDir);
            do
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                yield return null;
            } while (transform.rotation != rotation);
        }

        private void ClearTiles()
        {
            foreach (GameObject tile in pathVisualized)
            {
                Destroy(tile);
            }

            pathVisualized.Clear();
        }
    }
}
