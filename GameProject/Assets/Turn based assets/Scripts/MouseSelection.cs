using System;
using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class MouseSelection : MonoBehaviour
    {
        private ISelection _selectionResponse;
        private GameObject _selection;

        private void Awake()
        {
            _selectionResponse = GetComponent<ISelection>();
        }

        
        private void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit cube");
                    _selectionResponse.Select(hit.transform.gameObject);
                }
            }
        }
    }
}