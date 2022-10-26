using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TDUIManager : MonoBehaviour
{
    public TMP_Text goldCount;
    public TMP_Text coreHealth;
    public TMP_Text roundCount;

    // Start is called before the first frame update
    void Start()
    {
        goldCount.SetText(gameObject.GetComponent<GameManager>().startCurrency.ToString());
        coreHealth.SetText(gameObject.GetComponent<GameManager>().coreHealth.ToString());
        roundCount.SetText(gameObject.GetComponent<GameManager>().round.ToString());
        gameObject.GetComponent<GameManager>().coreSpawned = false;
    }

    // Update is called once per frame
    void Update(){}

    //Don't need to update every frame
    void FixedUpdate()
    {
        goldCount.SetText(gameObject.GetComponent<GameManager>().startCurrency.ToString());
        coreHealth.SetText(gameObject.GetComponent<GameManager>().coreHealth.ToString());
        roundCount.SetText(gameObject.GetComponent<GameManager>().round.ToString());
    }
}
