using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCore : MonoBehaviour
{
    public int MAX_HEALTH;
    public float gen_Time;
    int current_hp;

    public GameObject g_Manager;


    // Start is called before the first frame update
    void Start()
    {
        current_hp = MAX_HEALTH;
        g_Manager.GetComponent<GameManager>().currency = g_Manager.GetComponent<GameManager>().startCurrency;
        StartCoroutine(generateGold());
    }

    private IEnumerator generateGold()
    {
        while (current_hp > 0)
        {
            yield return new WaitForSeconds(gen_Time);
            g_Manager.GetComponent<GameManager>().currency += 50;
            current_hp--;
        }
    }

    //Generates gold for player every so often
    void FixedUpdate()
    {
        g_Manager.GetComponent<GameManager>().coreHealth = current_hp;
    }


}
