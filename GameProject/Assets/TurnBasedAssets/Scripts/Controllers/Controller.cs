using System;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class Controller : MonoBehaviour, IObjectAvoidanceInitialisable
    {
        internal IPathfinder Pathfinder;
        internal IPosition Position;
        private ObjectAvoidance _avoider;
        [SerializeField] internal MouseSelection mouseSelectionScript;
        [SerializeField] internal GameObject pathFinderTiles;
        [SerializeField] internal float movementSpeed;
        [SerializeField] internal float rotationSpeed;
        [SerializeField] internal bool isTerrain;
        public Renderer renderBounds => GetComponent<Renderer>();
        //public Mesh Mesh => GetComponent<MeshFilter>().mesh;
        private ObjectAvoidance objectAvoider;
        //private List<Vector3> MeshVertices => new List<Vector3>(Mesh.vertices);
        public List<Vector3> VectorPositions = new List<Vector3>();
        public TerrainData TerrainData => isTerrain ? GetComponent<Terrain>().terrainData : null;
        
        public Bounds terrainBounds => new Bounds(TerrainData.bounds.center + transform.position, TerrainData.bounds.size);
 
//        public void VertexLocations()
//        {
//            foreach (var vertex in MeshVertices)
//            {
//                var verteces = transform.TransformPoint(vertex);
//                VectorPositions.Add(verteces);
//            }
//        }

        public void AvoidMe()
        {
            _avoider.Objects.Add(this);
        }

        public void AddToObjectAvoider()
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new ObjectRequestMessage(this));
        }
        
        public void ObjectInitialise(ObjectAvoidance objectAvoider)
        {
            _avoider = objectAvoider;
        }
        
        public void PositionInitialise(IPosition iPosition)
        {
            Position = iPosition;
        }
        
        public void PathInitialise(IPathfinder pathfinder)
        {
            Pathfinder = pathfinder;
        }
    }
}
