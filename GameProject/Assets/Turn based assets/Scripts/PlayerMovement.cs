using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        public Vector3 currentTile;


        private void Start()
        {

        }


        private void Update()
        {
            MovePlayer();
            CheckNeighbours();
        }


        private void MovePlayer()
        {
            transform.position = currentTile;
        }

        private void CheckNeighbours()
        {
            
        }
    }
}
