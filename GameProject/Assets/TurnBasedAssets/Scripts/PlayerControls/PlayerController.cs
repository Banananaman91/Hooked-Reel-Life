using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PathFinder pathFinder;
        public Vector3 currentPos;
        private MouseSelection _mouseSelection;

        private void Start()
        {
            if(GetComponent<PathFinder>() != null)
                pathFinder = GetComponent<PathFinder>();

            currentPos = transform.position;
        }

        public void Update()
        {
            currentPos = transform.position;
        }

        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            IEnumerable<Vector3> path = new List<Vector3>();
            yield return StartCoroutine(routine: pathFinder.FindPath(transform.position, rawGridPoint, newPath => path = newPath));

            foreach (var LOCATION in path)
            {
                transform.position = Vector3.MoveTowards(transform.position, LOCATION, 1);
                //currentPos = LOCATION;
            }

            yield return null;
        }


        // Purely for fun
        private void YeetThePlayer()
        {
            if (_mouseSelection != null)
            {
                currentPos = _mouseSelection.rawGridPoint;
                transform.position = Vector3.MoveTowards(transform.position, currentPos, 1); // Y E E T
            }
        }
    }
}
