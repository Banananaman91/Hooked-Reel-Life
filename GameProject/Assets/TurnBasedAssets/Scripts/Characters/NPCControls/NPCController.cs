using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters.NPCControls
{
    public class NPCController : CharacterBase
    {
        private Vector3 _goal;
        public void NpcMove()
        {
            _isMoving = true;
//            FindGoal();
            StartCoroutine(FindPath(_goal));
            StartCoroutine(MoveCharacterAcrossPath());
            
        }

//        private Vector3 FindGoal()
//        {
//            var currentPosition = transform.position;
//            var newX = Random.Range(currentPosition.x, -+10);
//            var newZ = Random.Range(currentPosition.z, -+10);
//            return new Vector3(newX, currentPosition.y, newZ);
//        }

        private IEnumerator FindPath(Vector3 goal)
        {
            ClearVisualisedPath();

            yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, goal, false, moveableRadius, newPath => _path = newPath));

            foreach (var location in _path)
            {
                if (location == _goal) break;

                var tile = Instantiate(pathTilePrefab, location, Quaternion.identity);
                _visualisedPath.Add(tile);
            }
        }

        //private IEnumerable<Vector3> _path = new List<Vector3>();
        //private List<GameObject> _pathVisualised = new List<GameObject>();
        //private Vector3 _previousLocation;
        //private Vector3 _previousDistance;

        //public bool pathFound = false;

        //public IEnumerator FindPossibleMovePositions()
        //{
        //    ClearTiles();
        //    yield return StartCoroutine(routine: Pathfinder.FindPath(transform.position, _goalDestination.transform.position, false, 10, newPath => _path = newPath));
        //    pathFound = true;

        //    foreach (var location in _path)
        //    {
        //        if (location == _goalDestination.transform.position) break;
        //        var tile = Instantiate(pathFinderTiles, location, Quaternion.identity);
        //        _pathVisualised.Add(tile);
        //    }

        //    yield return null;
        //}


        //public IEnumerator StartNPCMovement()
        //{
        //    mouseSelectionScript.enabled = false;

        //    foreach (var location in _path)
        //    {
        //        Vector3 locationDistance = location - _previousLocation;

        //        if(locationDistance != _previousDistance)
        //        {
        //            yield return StartCoroutine(RotateNPC(location));
        //        }

        //        while(transform.position != location)
        //        {
        //            yield return StartCoroutine(MoveToNextTile(location));
        //        }

        //        _previousDistance = location - _previousLocation;
        //        _previousLocation = location;
        //    }

        //    mouseSelectionScript.enabled = true;
        //    ClearTiles();
        //    mouseSelectionScript.Selection.DeSelect();

        //    yield return null;
        //}


        //private IEnumerator MoveToNextTile(Vector3 location)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
        //    yield return null;
        //}


        //private IEnumerator RotateNPC(Vector3 location)
        //{
        //    Vector3 targetDir = location - transform.position;
        //    Quaternion rotation = Quaternion.LookRotation(targetDir);

        //    do
        //    {
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        //        yield return null;
        //    } while (transform.rotation != rotation);

        //    yield return null;
        //}


        //private void ClearTiles()
        //{
        //    foreach (GameObject tile in _pathVisualised)
        //    {
        //        Destroy(tile);
        //    }

        //    _pathVisualised.Clear();
        //}

    }

}
