using System;
using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private GameObject cube;
        public float distanceY;
        public float offset;
        public float gridSize;
        private Plane _plane;
        private Vector3 distanceFromCamera;
        

        private void Start()
        {
            var position = Camera.main.transform.position;
            distanceFromCamera = new Vector3(position.x, position.y - distanceY,position.z);
            _plane = new Plane(Vector3.up, distanceFromCamera);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float gridSpace = 0;

            if (_plane.Raycast(ray, out gridSpace))
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
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent<ISelection>() != null)
                    {
                        if (_selection != null) _selection.DeSelect();
                        _selection = hit.collider.GetComponent<ISelection>();
                        _selection.Select();
                    }
                }
                else
                {
                    if (_selection != null) _selection.DeSelect();
                }
            }
        }
    }
}