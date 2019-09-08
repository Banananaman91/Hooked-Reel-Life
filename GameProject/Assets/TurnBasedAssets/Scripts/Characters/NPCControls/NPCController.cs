using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters.NPCControls
{
    public class NPCController : CharacterBase
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private List<GameObject> _NPCGoalPositions;
        private int currentGoalPositionIndex;

        private int _numberOfTilesMoved;



        public override IEnumerator VisualisePath()
        {
            ClearVisualisedPath();
            goalPosition = DetermineGoalPosition();

            if (_characterType == CharacterType.StalkingNPC) Debug.Log("begun finding stalker path");

            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, goalPosition, false, moveableRadius, newPath => _path = newPath));

            if (_characterType == CharacterType.StalkingNPC) Debug.Log("stalker path found");

            foreach (var location in _path)
            {
                if (location == goalPosition) break;

                var tile = Instantiate(pathTilePrefab, location, Quaternion.identity);
                tilesInPath++;
                _visualisedPath.Add(tile);
            }

            StartCoroutine(MoveCharacterAcrossPath());

            yield return null;
        }


        private Vector3 DetermineGoalPosition()
        {
            Vector3 determinedPosition;

            switch (_characterType)
            {
                case CharacterType.NPC:
                    determinedPosition = _NPCGoalPositions[0].transform.position;
                    break;

                case CharacterType.PatrollingNPC:
                    determinedPosition = _NPCGoalPositions[currentGoalPositionIndex].transform.position;
                    break;

                case CharacterType.StalkingNPC:
                    determinedPosition = _playerController.transform.position;
                    if (_characterType == CharacterType.StalkingNPC) Debug.Log("stalker goal found");
                    break;

                default:
                    determinedPosition = Vector3.zero;
                    break;
            }

            return determinedPosition;
        }


        public override IEnumerator MoveCharacterAcrossPath()
        {
            if (_characterType == CharacterType.StalkingNPC) Debug.Log("stalker moving");

            foreach (var location in _path)
            {
                if(_numberOfTilesMoved < _playerController.tilesInPath)
                {
                    Vector3 locationDistance = location - _previousLocation;

                    if (locationDistance != _previousDistance)
                    {
                        yield return StartCoroutine(RotateCharacter(location));
                    }

                    while (transform.position != location)
                    {
                        yield return StartCoroutine(MoveCharacterToNextTile(location));
                    }

                    _previousDistance = location - _previousLocation;
                    _previousLocation = location;
                }

                _numberOfTilesMoved++;
            }

            _numberOfTilesMoved = 0;

            switch (_characterType)
            {
                case CharacterType.NPC:
                    break;

                // Resets PatrollingNPC goal position loops
                case CharacterType.PatrollingNPC:
                    if (transform.position == goalPosition)
                        if (currentGoalPositionIndex == _NPCGoalPositions.Count - 1)
                            currentGoalPositionIndex = 0;
                        else
                            currentGoalPositionIndex++;
                    break;

                case CharacterType.StalkingNPC:
                    break;

                default:
                    break;
            }

            ClearVisualisedPath();
            mouseSelectionScript.enabled = true;

            yield return null;
        }
    }

}
