using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters.PlayerControls
{
    public class PlayerController : Controller
    {
        private IEnumerable<Vector3> _path = new List<Vector3>();
        private List<GameObject> _pathVisualized = new List<GameObject>();
        private Vector3 _previousLocation;
        private Vector3 _previousDistance;
        private float SphereRadius => mouseSelectionScript.MovableRadius;
        
        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            ClearTiles();
            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, rawGridPoint, false, SphereRadius,newPath => _path = newPath));
            foreach (var location in _path)
            {
                if (location == rawGridPoint) break;
                var tile = Instantiate(pathFinderTiles, location, Quaternion.identity);
                _pathVisualized.Add(tile);
            }
            yield return null;
        }

        public IEnumerator StartPlayerMovement()
        {
            mouseSelectionScript.enabled = false;
            foreach (var location in _path)
            {
                Vector3 locationDistance = location - _previousLocation;
                if (locationDistance != _previousDistance)
                {
                    yield return StartCoroutine(RotatePlayer(location));
                }

                while (transform.position != location)
                {
                    yield return StartCoroutine(MoveToNextTile(location));
                }

                _previousDistance = location - _previousLocation;
                _previousLocation = location;

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
            foreach (GameObject tile in _pathVisualized)
            {
                Destroy(tile);
            }

            _pathVisualized.Clear();
        }
    }
}
