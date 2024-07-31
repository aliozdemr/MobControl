using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;


public class Pervane : MonoBehaviour
{
    public Animator myAnimator;
    public BoxCollider myBox;
    System.Random rand;
    private void Start()
    {
        rand = new System.Random();

        StartCoroutine(beklemeCor(gameObject.tag));


    }
   

        private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("AltKarakter") & (gameObject.CompareTag("SolPervaneCollider")))
        {
            
            other.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-4f, 0, 0),ForceMode.Impulse);
        }

        else if(other.CompareTag("AltKarakter") & (gameObject.CompareTag("SagPervaneCollider")))
            other.transform.GetComponent<Rigidbody>().AddForce(new Vector3(4f, 0, 0), ForceMode.Impulse);
    }
    
   

    IEnumerator beklemeCor(string tag)
    {
        double bekleme = rand.NextDouble() + .5f;
        Debug.Log(bekleme + "," + tag);
        myBox.enabled = true;
        myAnimator.SetBool("Donme", true);
        yield return new WaitForSeconds((float)bekleme);
        myBox.enabled = false;
        myAnimator.SetBool("Donme", false);
        yield return new WaitForSeconds((float)bekleme);
        StartCoroutine(beklemeCor(tag));

    }

}
