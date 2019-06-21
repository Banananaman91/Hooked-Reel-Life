using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject moveTilePrefab;

        public Vector3 currentPos;
        private List<Vector3> possibleMoves = new List<Vector3>();
        private List<GameObject> moveTiles = new List<GameObject>();

        private MouseSelection mouseSelection;

        private bool findPossibleMoves;


        private void Start()
        {
            currentPos = new Vector3(0, 0, 0);
            transform.position = currentPos;

            findPossibleMoves = true;

            // Only needed for YeetThePlayer()
            if(FindObjectOfType<MouseSelection>() != null)
            {
                mouseSelection = FindObjectOfType<MouseSelection>();
            }
        }


        private void Update()
        {
            if(findPossibleMoves)
                FindPossibleMovePositions();
        }
        

        public void MovePlayer()
        {
            transform.position = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);

            findPossibleMoves = true;
        }


        private void FindPossibleMovePositions()
        {
            // Reset move options
            possibleMoves.Clear();

            foreach(GameObject tile in moveTiles)
            {
                Destroy(tile);
            }

            moveTiles.Clear();

            // Find new move options
            possibleMoves.Add(currentPos + Vector3.forward);
            possibleMoves.Add(currentPos + Vector3.right);
            possibleMoves.Add(currentPos + Vector3.back);
            possibleMoves.Add(currentPos + Vector3.left);

            foreach (Vector3 newTilePos in possibleMoves)
            {
                GameObject newTile = Instantiate(moveTilePrefab, newTilePos, Quaternion.identity);
                moveTiles.Add(newTile);
            }

            findPossibleMoves = false;
        }


        // Purely for fun
        private void YeetThePlayer()
        {
            currentPos = mouseSelection.rawGridPoint;
            transform.position = Vector3.MoveTowards(transform.position, currentPos, 1); // Y E E T
        }
    }
}
