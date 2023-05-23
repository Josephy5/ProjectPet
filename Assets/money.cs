using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    public int AmountOfMoney=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Addmoney(int num)
    {
        AmountOfMoney += num;
    }
    public void MinusMoney(int num)
    {
        AmountOfMoney -= num;
    }
    public int getMoney()
    {
        return AmountOfMoney;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
