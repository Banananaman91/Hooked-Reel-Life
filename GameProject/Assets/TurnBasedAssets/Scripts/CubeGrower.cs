using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts;
using UnityEngine;

public class CubeGrower : MonoBehaviour, ISelection
{
    [SerializeField] private Transform cubeSize;
    [SerializeField] private const int size = 3;
    public void Select()
    {
        cubeSize.localScale = this.transform.localScale * size;
    }

    public void DeSelect()
    {
        cubeSize.localScale = this.transform.localScale / size;
    }
}
