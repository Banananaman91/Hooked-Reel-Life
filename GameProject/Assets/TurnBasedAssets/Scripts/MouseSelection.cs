using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] GameObject cube;
        [SerializeField] float distanceY;
        [SerializeField] float offset;
        [SerializeField] float gridSize;
        [SerializeField] Camera camera;
        private Plane _plane;
        private Vector3 _distanceFromCamera;
        

        private void Start()
        {
            var position = camera.transform.position;
            _distanceFromCamera = new Vector3(position.x, position.y - distanceY,position.z);
            _plane = new Plane(Vector3.up, _distanceFromCamera);
        }

        private void Update()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out float gridSpace))
            {
                Vector3 gridPoint = ray.GetPoint(gridSpace);
                gridPoint -= Vector3.one * offset;
                gridPoint /= gridSize;
                gridPoint = new Vector3(Mathf.Round(gridPoint.x), Mathf.Round(gridPoint.y), Mathf.Round(gridPoint.z));
                gridPoint *= gridSize;
                gridPoint += Vector3.one * offset;
                if (cube != null) cube.transform.position = gridPoint;
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
                else if (_selection != null) _selection.DeSelect();
                
            }
        }
    }
}