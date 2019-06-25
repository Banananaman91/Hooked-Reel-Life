using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts;
using UnityEngine;

public class CubeGrower : MonoBehaviour, ISelection
{
    [SerializeField] private Transform _cubeSize;
    [SerializeField] private int _size;
    public void Select()
    {
        _cubeSize.localScale = this.transform.localScale * _size;
    }

    public void DeSelect()
    {
        _cubeSize.localScale = this.transform.localScale / _size;
    }
}
