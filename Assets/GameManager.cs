using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public money moneyScr;
    public TextMeshProUGUI moneyText;
    public Transform foodSpawnPoint;
    public GameObject food;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        moneyText.text = "Money: " +moneyScr.AmountOfMoney;
    }
    public void spawnFood()
    {
        moneyScr.MinusMoney(1);
        Instantiate(food, foodSpawnPoint);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
