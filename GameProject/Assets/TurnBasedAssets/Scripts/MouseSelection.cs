using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private GameObject _cube;
        [SerializeField] private float _distanceY;
        [SerializeField] private float _offset;
        [SerializeField] private float _gridSize;
        [SerializeField] private Camera _camera;
        private Plane _plane;
        private Vector3 _distanceFromCamera;
        

        private void Start()
        {
            var position = _camera.transform.position;
            _distanceFromCamera = new Vector3(position.x, position.y - _distanceY,position.z);
            _plane = new Plane(Vector3.up, _distanceFromCamera);
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out float gridSpace))
            {
                Vector3 rawGridPoint = ray.GetPoint(gridSpace);
                rawGridPoint -= Vector3.one * _offset;
                rawGridPoint /= _gridSize;
                rawGridPoint = new Vector3(Mathf.Round(rawGridPoint.x), Mathf.Round(rawGridPoint.y), Mathf.Round(rawGridPoint.z));
                rawGridPoint *= _gridSize;
                rawGridPoint += Vector3.one * _offset;
                if (_cube != null) _cube.transform.position = rawGridPoint;
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var selection = hit.collider.GetComponent<ISelection>();
                    if (selection != null)
                    {
                        if (_selection != null) _selection.DeSelect();
                        _selection = selection;
                        _selection.Select();
                    }
                }
                else if (_selection != null)
                {
                    _selection.DeSelect();
                }
                
            }
        }
    }
}