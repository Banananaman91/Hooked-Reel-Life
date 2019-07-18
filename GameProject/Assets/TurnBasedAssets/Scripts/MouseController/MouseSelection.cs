using System;
using UnityEngine;

namespace TurnBasedAssets.Scripts.MouseController
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private PlayerControls.PlayerController _player;
        [SerializeField] private Camera _mainCamera;
        private Vector3 _rawGridPoint;
        private Vector3 _previousGridPoint;
        [SerializeField] private float movableRadius;
        [SerializeField] private float distanceY;
        [SerializeField] private float offset;
        [SerializeField] private float gridSize;
        [SerializeField] private GameObject selectedTile;
        private Plane _plane;
        private Vector3 _distanceFromCamera;
        public Vector3 PlanePosition => _distanceFromCamera;
        public Vector3 CameraPosition => _mainCamera.transform.position;
        public Vector3 RawGridPoint => _rawGridPoint;


        private void Start()
        {
            _mainCamera.transform.position = new Vector3(CameraPosition.x,(int)Math.Round((CameraPosition.y)),CameraPosition.z);
            var position = Camera.main.transform.position;
            _distanceFromCamera = new Vector3(position.x, position.y - distanceY,position.z);
            _plane = new Plane(Vector3.up, _distanceFromCamera);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ;
            RaycastHit hit;
            if (_plane.Raycast(ray, out float gridSpace))
            {
                _rawGridPoint = CalculateGridPoint(ray, gridSpace);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Vector3.Distance(_player.transform.position, _rawGridPoint) <= movableRadius)
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.GetComponent<ISelection>() != null)
                            {
                                _selection = hit.collider.GetComponent<ISelection>();
                                _selection.Select();
                            }
                            //_selection?.DeSelect();
                        }
                        
                        else
                        {
                            _selection?.DeSelect();
                            var newTile = Instantiate(selectedTile, _rawGridPoint, Quaternion.identity);
                            _selection = newTile.GetComponent<ISelection>();
                        }
                    }

                
                    else
                    {
                        Debug.Log("This is too far away");
                    }
                }
            }
        }



        public Vector3 CalculateGridPoint(Ray ray, float gridSpace)
        {
            Vector3 gridPoint;

            gridPoint = ray.GetPoint(gridSpace);
            gridPoint -= Vector3.one * offset;
            gridPoint /= gridSize;
            gridPoint = new Vector3(Mathf.Round(gridPoint.x), Mathf.Round(gridPoint.y), Mathf.Round(gridPoint.z));
            gridPoint *= gridSize;
            gridPoint += Vector3.one * offset;

            return gridPoint;
        }
    }
}