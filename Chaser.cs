using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour {

    GameObject player1;
    GameObject player2;

    NavMeshAgent agent;

    Transform goal1;

    Transform goal2;

    private Animator animator;

    bool players;

    // Use this for initialization
    void Start () {
        players = PlayerData.Players;

        agent = GetComponent<NavMeshAgent>();

        player1 = GameObject.Find("Player1");
        goal1 = player1.transform;

        if (players)
        {
            player2 = GameObject.Find("Player2");
            goal2 = player2.transform;
        }    

        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update () {
        if (players)
        {
            if (Vector3.Distance(goal1.position, gameObject.transform.position) < 1.1)
            {
                agent.SetDestination(goal1.position);
                animator.SetBool("attack", true);
                print("attack");
            }
            else if (Vector3.Distance(goal2.position, gameObject.transform.position) < 1.1)
            {
                agent.SetDestination(goal2.position);
                animator.SetBool("attack", true);
                print("attack");
            }
            else if (Vector3.Distance(goal1.position, gameObject.transform.position) < 10)
            {
                agent.SetDestination(goal1.position);
                animator.SetFloat("Speed", 1);
                animator.SetBool("attack", false);
            }
            else if (Vector3.Distance(goal2.position, gameObject.transform.position) < 10)
            {
                agent.SetDestination(goal2.position);
                animator.SetFloat("Speed", 1);
                animator.SetBool("attack", false);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }
        else
        {
            if (Vector3.Distance(goal1.position, gameObject.transform.position) < 1.1)
            {
                agent.SetDestination(goal1.position);
                animator.SetBool("attack", true);
                print("attack");
            }
            else if (Vector3.Distance(goal1.position, gameObject.transform.position) < 10)
            {
                agent.SetDestination(goal1.position);
                animator.SetFloat("Speed", 1);
                animator.SetBool("attack", false);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.name == "Player1")
            {
                other.gameObject.GetComponent<Player1Respawn>().respawn();
            }
            else
            {
                other.gameObject.GetComponent<Player2Respawn>().respawn();
            }
        }
    }

}
