using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _moveTilePrefab;

        public Vector3 currentPos;
        private List<Vector3> _possibleMoves = new List<Vector3>();
        private List<GameObject> _moveTiles = new List<GameObject>();
        private float _minDist = Mathf.Infinity;
        private MouseSelection _mouseSelection;
        private Vector3 _newPosition;
        [SerializeField] private int maxDistance;


        private void Start()
        {
            currentPos = transform.position;
        }


        private void Update()
        {
//            if(_findPossibleMoves)
//                FindPossibleMovePositions();
        }
        

        public void MovePlayer(Vector3 rawGridPoint)
        {
            foreach (var possibleMoves in _possibleMoves )
            {
                float distance = Vector3.Distance(possibleMoves, rawGridPoint);
                if (distance < _minDist)
                {
                    _newPosition = possibleMoves;
                    _minDist = distance;
                }
            }
            
            Vector3.MoveTowards(transform.position, _newPosition, _minDist);
        }
        
        public void FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            // Reset move options
            _possibleMoves.Clear();

            foreach(GameObject tile in _moveTiles)
            {
                Destroy(tile);
            }

            _moveTiles.Clear();
            Vector3 route = (currentPos + rawGridPoint).normalized;

            _possibleMoves.Add(currentPos + route);

            foreach (Vector3 newTilePos in _possibleMoves)
            {
                GameObject newTile = Instantiate(_moveTilePrefab, newTilePos, Quaternion.identity);
                _moveTiles.Add(newTile);
            }
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
