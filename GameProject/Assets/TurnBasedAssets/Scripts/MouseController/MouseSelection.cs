using UnityEngine;

namespace TurnBasedAssets.Scripts.MouseController
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private PlayerControls.PlayerController _player;

        private Vector3 _rawGridPoint;
        private Vector3 _previousGridPoint;
        [SerializeField] private float movableRadius;
        [SerializeField] private float distanceY;
        [SerializeField] private float offset;
        [SerializeField] private float gridSize;
        private Plane _plane;
        private Vector3 _distanceFromCamera;
        

        private void Start()
        {
            var position = Camera.main.transform.position;
            _distanceFromCamera = new Vector3(position.x, position.y - distanceY,position.z);
            _plane = new Plane(Vector3.up, _distanceFromCamera);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;

            if (_plane.Raycast(ray, out float gridSpace))
            {
                _rawGridPoint = CalculateGridPoint(ray, gridSpace);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Vector3.Distance(_player.transform.position, _rawGridPoint) <= movableRadius)
                    {
                        StartCoroutine(_player.FindPossibleMovePositions(_rawGridPoint));
                    }
                    else
                    {
                        Debug.Log("This is too far away");
                    }
                }
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