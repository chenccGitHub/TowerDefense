using UnityEngine;
using UnityEngine.EventSystems;
public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Color prohibitColor;
    private Renderer rend;
    private Color startColor;
    [HideInInspector]
    public GameObject turret;
    private BuildManager buildManager;
    public Vector3 offectPosition;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + offectPosition;
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!buildManager.canBuild)
        {
            return;
        }
        if (buildManager.canMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = prohibitColor;
        }
        
    }
    void OnMouseExit()
    {
        if (!buildManager.canBuild)
        {
            return;
        }
        rend.material.color = startColor;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!buildManager.canBuild)
        {
            Debug.Log("不买炮台，想白嫖？不存在的！");
            return;
        }
        if (turret != null)
        {
            Debug.Log("当前位置已经有炮台了！");
            return;
        }
        buildManager.BuildTurretOn(this);
    }
}
