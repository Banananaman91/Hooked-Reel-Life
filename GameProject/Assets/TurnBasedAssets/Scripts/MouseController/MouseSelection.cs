using System;
using TurnBasedAssets.Scripts.Controllers;
using UnityEngine;

namespace TurnBasedAssets.Scripts.MouseController
{
    public class MouseSelection : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _movableRadius;
        [SerializeField] private float _distanceY;
        [SerializeField] private float _offset;
        [SerializeField] private float _gridSize;
        [SerializeField] private GameObject _selectedTile;
        private ISelection _selection;
        private Plane _plane;
        private Vector3 _rawGridPoint;
        private Vector3 _previousGridPoint;
        private Vector3 _distanceFromCamera;
        public Vector3 PlanePosition => _distanceFromCamera;
        private Vector3 CameraPosition => _mainCamera.transform.position;
        public Vector3 RawGridPoint => _rawGridPoint;

        public ISelection Selection
        {
            get => _selection;
            set => _selection = value;
        }
        public float MovableRadius => _movableRadius;


        private void Awake()
        {
            _mainCamera.transform.position =
                new Vector3(CameraPosition.x, (int) Math.Round((CameraPosition.y)), CameraPosition.z);
            var position = Camera.main.transform.position;
            _distanceFromCamera = new Vector3(position.x, position.y - _distanceY, position.z);
            _plane = new Plane(Vector3.up, _distanceFromCamera);
        }


        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_plane.Raycast(ray, out float gridSpace))
            {
                _rawGridPoint = CalculateGridPoint(ray, gridSpace);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Vector3.Distance(_player.transform.position, _rawGridPoint) <= _movableRadius)
                {
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.GetComponent<ISelection>() != null)
                        {
                            _selection = hit.collider.GetComponent<ISelection>();
                            _selection.Select();
                        }

                        else if (!hit.collider.GetComponent<ObjectPositioner>())
                        {
                            _selection?.DeSelect();
                            var newTile = Instantiate(_selectedTile, _rawGridPoint, Quaternion.identity);
                            _selection = newTile.GetComponent<ISelection>();
                        }
                    }

                    else
                    {
                        _selection?.DeSelect();
                        var newTile = Instantiate(_selectedTile, _rawGridPoint, Quaternion.identity);
                        _selection = newTile.GetComponent<ISelection>();
                    }
                }
            }
        }
        
        public Vector3 CalculateGridPoint(Ray ray, float gridSpace)
        {
            Vector3 gridPoint;

            gridPoint = ray.GetPoint(gridSpace);
            gridPoint -= Vector3.one * _offset;
            gridPoint /= _gridSize;
            gridPoint = new Vector3(Mathf.Round(gridPoint.x), Mathf.Round(gridPoint.y), Mathf.Round(gridPoint.z));
            gridPoint *= _gridSize;
            gridPoint += Vector3.one * _offset;

            return gridPoint;
        }
    }
}