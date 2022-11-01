using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public Transform[] points;
    private Animator animator;      // Component controlling player animations
    private NavMeshAgent nav;
    private int destPoint;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.SetTrigger("Run");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.GetComponent<SST_Actor>().toughness < 0)
        {
            GameManager.Instance.currency += 50;
            Destroy(gameObject);
        }
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    //Updates target for actor to reach
    void GoToNextPoint()
    {
        //Double checks that list is empty to prevent errors
        if(points.Length == 0)
        {
            return;
        }
        int prevDest = destPoint;
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
        if(prevDest - 1 > destPoint)
        {
            GameManager.Instance.coreHealth -= 10;
            Destroy(gameObject);
        }
    }
}
