using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        private IPathFinder _iPathFinder;
        private Vector3 _currentPos;
        [SerializeField] private MouseSelection mouseSelectionScript;
        [SerializeField] private float movementSpeed;
        private float SphereRadius => mouseSelectionScript.MoveableRadius;

        private void Start()
        {
            PathMessenger.Instance.SendMessageOfType(new PathFinderRequestMessage(this));
            transform.position = new Vector3(transform.position.x, mouseSelectionScript.PlanePosition.y, transform.position.z);
        }
        
        public void Initialise(IPathFinder pathFinder)
        {
            _iPathFinder = pathFinder;
        }

        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            List<Vector3> hitPositions = CheckPositions(rawGridPoint);
            mouseSelectionScript.enabled = false;
            IEnumerable<Vector3> path = new List<Vector3>();
            yield return StartCoroutine(routine: _iPathFinder.FindPath(transform.position, rawGridPoint, false, hitPositions,newPath => path = newPath));
            foreach (var LOCATION in path)
            {
                while (transform.position != LOCATION)
                {
                    yield return StartCoroutine(MoveToNextTile(LOCATION));
                }
            }

            mouseSelectionScript.enabled = true;
            yield return null;
        }

        private IEnumerator MoveToNextTile(Vector3 location)
        {
            transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
            yield return null;
        }

        private List<Vector3> CheckPositions(Vector3 rawGridPoint)
        {
            List<RaycastHit> hits = new List<RaycastHit>(Physics.SphereCastAll(transform.position, SphereRadius, rawGridPoint));
            List<Vector3> hitPositions = new List<Vector3>();
            foreach (var hit in hits)
            {
                hitPositions.Add(hit.transform.position);
            }

            return hitPositions;
        }
    }
}
