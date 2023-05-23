using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pet : MonoBehaviour
{
    public int Hunger=100;
    public int health=5;
    public TextMeshProUGUI hungerText;
    public PetAi scr;

    private bool hasExecuted;

    float lastHungerUpdateTime = 0f;
    float currentTime;

    void Start()
    {
        hasExecuted = false;
        //InvokeRepeating("GettingHungry", 0, 30f);
    }
    /*void GettingHungry()
    {
        if (!(Hunger <= 0))
        {
            Hunger -= 1;
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food"&& hasExecuted==false)
        {
            Debug.Log("Pet eating");
            hasExecuted = true;
            scr.stopPatrolling();
            if (!(Hunger >= 100))
            {
                Hunger += 5;
            }
            //Hunger += Mathf.Clamp(5,0,100);
            collision.gameObject.GetComponent<Food>().StartCoroutine("beingEaten");
        }
    }
    public void noLongerEating()
    {
        scr.resumePatrolling();
        hasExecuted = false;
    }
    private void FixedUpdate()
    {
        hungerText.text = "Hunger: " + Hunger;

        currentTime = Time.time;

        if (currentTime - lastHungerUpdateTime > 30f)
        {
            Hunger -= 1;
            lastHungerUpdateTime = currentTime;
        }

        if (Hunger < 1)
        {
            // do whatever
        }
    }
}
