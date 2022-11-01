using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDUIManager : MonoBehaviour
{
    public TextMesh goldCount;
    public TextMesh coreHealth;
    public TextMesh roundCount;

    // Start is called before the first frame update
    void Start()
    {
        goldCount.text = (gameObject.GetComponent<GameManager>().startCurrency.ToString());
        coreHealth.text = (gameObject.GetComponent<GameManager>().coreHealth.ToString());
        roundCount.text = (gameObject.GetComponent<GameManager>().round.ToString());
        gameObject.GetComponent<GameManager>().coreSpawned = false;
    }

    // Update is called once per frame
    void Update(){}

    //Don't need to update every frame
    void FixedUpdate()
    {
        goldCount.text = (gameObject.GetComponent<GameManager>().startCurrency.ToString());
        coreHealth.text = (gameObject.GetComponent<GameManager>().coreHealth.ToString());
        roundCount.text = (gameObject.GetComponent<GameManager>().round.ToString());
    }
}
