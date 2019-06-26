using UnityEngine;

namespace TurnBasedAssets.Scripts.MouseController
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selection;
        [SerializeField] private PlayerController.PlayerController player;
        public Vector3 rawGridPoint;
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
                rawGridPoint = ray.GetPoint(gridSpace);
                rawGridPoint -= Vector3.one * offset;
                rawGridPoint /= gridSize;
                rawGridPoint = new Vector3(Mathf.Round(rawGridPoint.x), Mathf.Round(rawGridPoint.y), Mathf.Round(rawGridPoint.z));
                rawGridPoint *= gridSize;
                rawGridPoint += Vector3.one * offset;
                player.FindPossibleMovePositions(rawGridPoint);
                if(Input.GetKeyDown(KeyCode.Mouse0)) player.MovePlayer(rawGridPoint);
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
    }
}