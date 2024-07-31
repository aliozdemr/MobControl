using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Dusman : MonoBehaviour
{
    public GameObject SaldiriHedefi;
    NavMeshAgent agent;
    bool SaldiriBasladiMi;
    public GameManager manager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void AnimasyonTetikleme()
    {
        GetComponent<Animator>().SetBool("Saldir", true);
        SaldiriBasladiMi = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (SaldiriBasladiMi)
        {
            agent.SetDestination(SaldiriHedefi.transform.position);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("AnaKarakter"))
        {
            other.gameObject.SetActive(false) ;
            gameObject.SetActive(false);
            manager.DusmanSayisi--;
            manager.KarakterSayisi--;
        }
        else if (other.CompareTag("AltKarakter"))
        {
            gameObject.SetActive(false);
            manager.DusmanSayisi--;
        }
    }
}
