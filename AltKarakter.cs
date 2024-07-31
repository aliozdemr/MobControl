using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AltKarakter : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;
    public GameObject managerObject;
    GameManager manager;
    void Start()
    {
        manager = managerObject.GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().VarisNoktasi;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Engel") || (other.CompareTag("Dusman")))
        {
            manager.YokOlmaEfektiOlusturma(other.transform, other.tag);
            gameObject.SetActive(false);
            manager.KarakterSayisi--;
        }
    }
}
