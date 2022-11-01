using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCore : MonoBehaviour
{
    public int MAX_HEALTH;
    public float gen_Time;
    public int gen_amnt;
    public GameObject minion;
    public GameObject minionPortal;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.coreHealth = MAX_HEALTH;
        GameManager.Instance.currency = GameManager.Instance.startCurrency;
        StartCoroutine(generateGold());
        StartCoroutine(generateMinions());
    }

    private IEnumerator generateGold()
    {
        while (GameManager.Instance.coreHealth > 0)
        {
            yield return new WaitForSeconds(gen_Time);
            GameManager.Instance.currency += gen_amnt;
            GameManager.Instance.coreHealth--;
        }
    }

    private IEnumerator generateMinions()
    {
        while (GameManager.Instance.coreHealth > 0)
        {
            yield return new WaitForSeconds(gen_Time / 3);
            GameObject tempMin = Instantiate(minion, minionPortal.transform.position, Quaternion.identity);
            tempMin.GetComponent<Pathfinding>().points = GameManager.Instance.points;
        }
    }

    //Generates gold for player every so often
    void FixedUpdate()
    {
    }


}
