using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildManager:MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    public bool canBuild { get { return turretToBuild != null; } }
    public bool canMoney { get { return PlayerStats.money >= turretToBuild.cost; } }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    public void BuildTurretOn(Nodes node)
    {
        if (PlayerStats.CalculateMoney(turretToBuild.cost))
        {
            //弹出提示，没有足够的金钱来购买炮台
            return;
        }
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }
}
