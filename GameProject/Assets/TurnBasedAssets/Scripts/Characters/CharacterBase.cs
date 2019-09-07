using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.SelectedTile;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters
{
    public class CharacterBase : Controller
    {
        // Player Information
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected float rotationSpeed;
        // Other variables to add here: health, name, etc etc

        // Player Type Variables
        enum CharacterTypes { Player, NPC };
        [SerializeField] private CharacterTypes _characterType;

        // Path Variables
        protected float moveableRadius;
        [SerializeField] protected GameObject pathTilePrefab;
        private IEnumerable<Vector3> _path = new List<Vector3>();
        private List<GameObject> _visualisedPath = new List<GameObject>();
        private Vector3 _previousLocation, _previousDistance;

        public bool _playerFinishedMove = false; // public for testing
        public bool _turnStarted = false; // public for testing

        private void Update()
        {
            if(!_turnStarted)
            {
                _turnStarted = true;
                MoveManager();
                _turnStarted = false;
            }
        }


        private void MoveManager()
        {
            if (FindObjectOfType<SelectTile>() != null)
            {
                if(FindObjectOfType<SelectTile>().confirmedMove)
                {
                    switch (_characterType)
                    {
                        case CharacterTypes.Player:
                            _playerFinishedMove = false;
                            StartCoroutine(MoveCharacterAcrossPath());
                            break;
                        case CharacterTypes.NPC:
                            if (_playerFinishedMove)
                                StartCoroutine(MoveCharacterAcrossPath());
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        public IEnumerator VisualisePath(Vector3 goalPosition)
        {
            ClearVisualisedPath();

            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, goalPosition, false, moveableRadius, newPath => _path = newPath));

            foreach (var location in _path)
            {
                if (location == goalPosition) break;

                var tile = Instantiate(pathTilePrefab, location, Quaternion.identity);
                _visualisedPath.Add(tile);
            }

            yield return null;
        }


        private IEnumerator MoveCharacterAcrossPath()
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

            _playerFinishedMove = true;
            mouseSelectionScript.enabled = true;
            ClearVisualisedPath();
            mouseSelectionScript.Selection.DeSelect();

            yield return null;
        }


        private IEnumerator MoveCharacterToNextTile(Vector3 location)
        {
            transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
            yield return null;
        }


        private IEnumerator RotateCharacter(Vector3 location)
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


        private void ClearVisualisedPath()
        {
            foreach (GameObject tile in _visualisedPath)
            {
                Destroy(tile);
            }

            _visualisedPath.Clear();
        }
    }
}
