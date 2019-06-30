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
        Action<IEnumerable<Vector3>> path;

        //// Unused:
        //private List<Vector3> _possibleMoves = new List<Vector3>();
        //private float _minDist = Mathf.Infinity;
        //private Vector3 _newPosition;
        //[SerializeField] private int maxDistance;


        private void Start()
        {
            if(GetComponent<PathFinder>() != null)
                pathFinder = GetComponent<PathFinder>();

            currentPos = transform.position;
        }


        private void Update()
        {
            //if (_findPossibleMoves)
            //    FindPossibleMovePositions()

            MovePlayer();
        }


        public void MovePlayer()
        {
            //foreach (var possibleMoves in _possibleMoves )
            //{
            //    float distance = Vector3.Distance(possibleMoves, rawGridPoint);
            //    if (distance < _minDist)
            //    {
            //        _newPosition = possibleMoves;
            //        _minDist = distance;
            //    }
            //}

            transform.position = Vector3.MoveTowards(transform.position, currentPos, 1);

            //for (int p = pathToFollow.Count - 1; p > -1; p--)
            //{
            //    currentPos = pathToFollow[p];
            //    transform.position = currentPos;
            //}
        }
        
        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            IEnumerable<Vector3> path = new List<Vector3>();
            yield return StartCoroutine(routine: pathFinder.FindPath(transform.position, rawGridPoint, newPath => path = newPath));

            foreach (var LOCATION in path)
            {
                //transform.position = Vector3.MoveTowards(transform.position, LOCATION, 1);
                currentPos = LOCATION;
                Debug.Log(LOCATION);
            }

            yield return null;

            //// Reset move options
            //_possibleMoves.Clear();

            //foreach (GameObject tile in _moveTiles)
            //{
            //    Destroy(tile);
            //}

            //_moveTiles.Clear();
            //Vector3 route = (currentPos + rawGridPoint).normalized;

            //_possibleMoves.Add(currentPos + route);

            //foreach (Vector3 newTilePos in _possibleMoves)
            //{
            //    GameObject newTile = Instantiate(_moveTilePrefab, newTilePos, Quaternion.identity);
            //    _moveTiles.Add(newTile);
            //}
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
