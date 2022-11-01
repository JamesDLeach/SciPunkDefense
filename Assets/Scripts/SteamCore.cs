using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCore : MonoBehaviour
{
    public int MAX_HEALTH;
    public float gen_Time;
    int current_hp;

    // Start is called before the first frame update
    void Start()
    {
        current_hp = MAX_HEALTH;
        GameManager.Instance.currency = GameManager.Instance.startCurrency;
        StartCoroutine(generateGold());
    }

    private IEnumerator generateGold()
    {
        while (current_hp > 0)
        {
            yield return new WaitForSeconds(gen_Time);
            GameManager.Instance.currency += 50;
            current_hp--;
        }
    }

    //Generates gold for player every so often
    void FixedUpdate()
    {
        GameManager.Instance.coreHealth = current_hp;
    }


}
