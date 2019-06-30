using UnityEngine;

namespace TurnBasedAssets.Scripts.MouseController
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private PlayerControls.PlayerController player;

        public Vector3 rawGridPoint;
        private Vector3 previousGridPoint;
        [SerializeField] private float movableRadius = 5;

        public float distanceY;
        public float offset;
        public float gridSize;
        public Plane _plane;
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
                rawGridPoint = CalculateGridPoint(ray, gridSpace);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (previousGridPoint != rawGridPoint)
                    {
                        if(Vector3.Distance(player.currentPos, rawGridPoint) <= movableRadius)
                        {
                            StartCoroutine(player.FindPossibleMovePositions(rawGridPoint));
                            previousGridPoint = rawGridPoint;
                        }
                        else
                        {
                            Debug.Log("This is too far away");
                        }
                    }
                }

                //// Makes player follow mouse
                //if (player != null) player.transform.position = rawGridPoint;
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