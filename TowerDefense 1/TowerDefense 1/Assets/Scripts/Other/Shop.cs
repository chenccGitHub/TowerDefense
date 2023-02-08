using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    private Button standardBtn;
    private Button anotherBtn;
    private Button laserBtn;
    public TurretBlueprint standardTurret;
    public TurretBlueprint anotherTurret;
    public TurretBlueprint laserTurret;
    public static Text moneyText;
    public static Text hpText;
    void Awake()
    {
        standardBtn = transform.Find("StandardTurret").GetComponent<Button>();
        anotherBtn = transform.Find("AnotherTurret").GetComponent<Button>();
        laserBtn = transform.Find("LaserTurret").GetComponent <Button>();
        moneyText = transform.Find("Money").GetComponent <Text>();
        hpText = transform.Find("Hp").GetComponent<Text>();
    }
    void Start()
    {
        buildManager = BuildManager.instance;
        standardBtn.onClick.AddListener(SelectStandardTurret);
        anotherBtn.onClick.AddListener(SelectAnotherTurret);
        laserBtn.onClick.AddListener(SelectLaserTurret);
    }
    /// <summary>
    /// 选择标准炮台
    /// </summary>
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    /// <summary>
    /// 选择其它炮台
    /// </summary>
    public void SelectAnotherTurret()
    {
        buildManager.SelectTurretToBuild(anotherTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
    public static void UpdateMoney(int _money)
    {
        moneyText.text = _money.ToString();
    }
    public static void UpdateHp(int hp)
    {
        hpText.text = hp.ToString();
    }
}
