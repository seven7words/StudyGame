using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerManager : BaseManager {

    public PlayerManager(GameFacade facade):base(facade)
    {
        
    }

    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    private Transform rolePositions;
    private UserData userData;
    private Dictionary<RoleType,RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Prefabs/Hunter_BLUE", "Prefabs/Arrow_BLUE", rolePositions.GetChild(0)));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Prefabs/Hunter_RED", "Prefabs/Arrow_RED", rolePositions.GetChild(1)));

    }

    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }

    public void SpawnRoles()
    {

        foreach (RoleData rd in roleDataDict.Values)
        {
            GameObject go =  GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);

            if (rd.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
            }
        }
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }
}
