using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RoleData
{

    public RoleType RoleType { get; private set; }
    public GameObject RolePrefab { get; private set; }
    public GameObject ArrowPrefab { get; private set; }
    public Vector3 SpawnPosition { get; private set; }
    public GameObject ExplostionEffect { get; private set; }
    public RoleData(RoleType roleType, string rolePath, string arrowPath,Transform spawnPosition,string explosionPath)
    {
        this.RoleType = roleType;
        this.RolePrefab = Resources.Load( rolePath) as GameObject;
        this.ArrowPrefab = Resources.Load(arrowPath) as GameObject;
        this.SpawnPosition = spawnPosition.position;
        this.ExplostionEffect = Resources.Load(explosionPath) as GameObject;
        ArrowPrefab.GetComponent<Arrow>().explosionEffect = ExplostionEffect;
    }

}
