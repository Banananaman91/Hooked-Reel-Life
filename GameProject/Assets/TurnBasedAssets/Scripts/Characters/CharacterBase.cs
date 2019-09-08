using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters
{
    public class CharacterBase : Controller
    {
        // == Player Type Variables ==
        [SerializeField] protected CharacterType _characterType;

        // == Player Information ==
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected float rotationSpeed;

        // == Path Variables ==
        [SerializeField] protected GameObject pathTilePrefab;
        [SerializeField] protected MoveManager _moveManager;
        public Vector3 goalPosition;
        protected float moveableRadius;
        protected IEnumerable<Vector3> _path = new List<Vector3>();
        public int tilesInPath;
        protected List<GameObject> _visualisedPath = new List<GameObject>();
        protected Vector3 _previousLocation, _previousDistance;



        public virtual IEnumerator VisualisePath()
        {
            ClearVisualisedPath();
            tilesInPath = 0;

            goalPosition = mouseSelectionScript.RawGridPoint;

            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, goalPosition, false, moveableRadius, newPath => _path = newPath));

            foreach (var location in _path)
            {
                if (location == goalPosition) break;

                var tile = Instantiate(pathTilePrefab, location, Quaternion.identity);
                tilesInPath++;
                _visualisedPath.Add(tile);
            }

            yield return null;
        }


        public virtual IEnumerator MoveCharacterAcrossPath()
        {
            mouseSelectionScript.enabled = false;

            foreach (var location in _path)
            {
                Vector3 locationDistance = location - _previousLocation;

                if(locationDistance != _previousDistance)
                {
                    yield return StartCoroutine(RotateCharacter(location));
                }

                while(transform.position != location)
                {
                    yield return StartCoroutine(MoveCharacterToNextTile(location));
                }

                _previousDistance = location - _previousLocation;
                _previousLocation = location;
            }

            ClearVisualisedPath();

            if(_characterType == CharacterType.Player)
            {
                _moveManager.StartCharacterMoves(false);
            }

            yield return null;
        }


        protected IEnumerator MoveCharacterToNextTile(Vector3 location)
        {
            transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
            yield return null;
        }


        protected IEnumerator RotateCharacter(Vector3 location)
        {
            Vector3 targetDir = location - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDir);

            do
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                yield return null;
            } while (transform.rotation != rotation);

            yield return null;
        }


        protected void ClearVisualisedPath()
        {
            foreach (GameObject tile in _visualisedPath)
            {
                Destroy(tile);
            }

            _visualisedPath.Clear();
        }
        
        private Vector3 FindGoal()
        {
            var currentPosition = transform.position;
            var newX = Random.Range(currentPosition.x, -+10);
            var newZ = Random.Range(currentPosition.z, -+10);
            return new Vector3(newX, currentPosition.y, newZ);
        }
    }
}
