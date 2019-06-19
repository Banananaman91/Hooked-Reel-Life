using System.Collections;
using System.Collections.Generic;
using Turn_based_assets.Scripts;
using UnityEngine;

public class CubeGrower : MonoBehaviour, ISelection
{
    public void Select()
    {
        this.transform.localScale = this.transform.localScale * 3;
    }

    public void DeSelect()
    {
        this.transform.localScale = this.transform.localScale / 3;
    }
}
