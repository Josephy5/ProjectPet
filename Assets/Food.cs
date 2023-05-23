using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Pet scr;
    // Start is called before the first frame update
    public void DestroyFood()
    {
        scr.noLongerEating();
        Destroy(gameObject);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        Debug.Log(this.name+" Destroyed");
    }
    IEnumerator beingEaten()
    {
        //play particle
        Vector3 decrease = new Vector3(0.01f, 0.01f, 0.01f);

        for (int i = 0; i < 15; i++)
        {
            gameObject.transform.localScale -= decrease;
            yield return new WaitForSeconds(0.5f);
        }
        //stop particle
        DestroyFood();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
