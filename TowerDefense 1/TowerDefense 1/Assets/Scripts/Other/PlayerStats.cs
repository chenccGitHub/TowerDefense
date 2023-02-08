using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int initMoney = 400;
    public static int curHp;
    public int maxHp = 10;
    private void Start()
    {
        money = initMoney;
        curHp = maxHp;
    }
    public static bool CalculateMoney(int price)
    {
        if (money < price)
        {
            return true;
        }
        money -= price;
        Shop.UpdateMoney(money);
        return false;
    }
    public static void CalculateHp(int damage)
    {
        if (curHp <= 0)
        {
            return;  
        }
        curHp -= damage;
        Shop.UpdateHp(curHp);
    }
}
