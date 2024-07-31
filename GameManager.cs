using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject VarisNoktasi;
    public GameObject DogusNoktasi;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> Dusmanlar;
    public int DusmanSayisi;
    public int KarakterSayisi;
    public GameObject DusmanSayisiText;
    public GameObject KarakterSayisiText;
    public GameObject DusmanKazandi;
    public GameObject SenKazandin;
    public GameObject Berabere;
    System.Random random;
    public bool SaldiriBasla;
    public AudioSource anaSes;

    
    void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < DusmanSayisi; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
        random = new System.Random();
        KarakterSayisi = 1;
    }

    
    void Update()
    {

        DusmanSayisiText.GetComponent<Text>().text = DusmanSayisi.ToString();
        KarakterSayisiText.GetComponent<Text>().text = KarakterSayisi.ToString();
        if(DusmanSayisi <=0 &&  KarakterSayisi <= 0)
        {
            Berabere.SetActive(true);
            anaSes.Stop();
            Time.timeScale = 0;
           
            
        }
        else if(DusmanSayisi <= 0)
        {
            SenKazandin.SetActive(true);
            anaSes.Stop();
            Time.timeScale = 0;


        }
        else if(KarakterSayisi <= 0)
        {
            DusmanKazandi.SetActive(true);
            anaSes.Stop();
            Time.timeScale = 0;

        }
        if (SaldiriBasla)
        {
            
            foreach (GameObject item in Dusmanlar)
            {
                if (item.activeInHierarchy)
                {
                    item.GetComponent<Dusman>().AnimasyonTetikleme();
                }
            }
            SaldiriBasla = false;
        }
    }
    public void TekrarOyna()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OyundanCık()
    {
        Application.Quit();
    }

    public void SonrakiLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator KimKazandi()
    {
        yield return new WaitForSeconds(5);
        if (DusmanSayisi > KarakterSayisi) 
            DusmanKazandi.SetActive(true);
        else
            SenKazandin.SetActive(true);

    }
    public void Carpma(int sayi, int karakterSayisi)
    {
        int eklenecekKarakterSayisi = (sayi * karakterSayisi) - karakterSayisi;
        while (eklenecekKarakterSayisi != 0)
        {
            for (int i = 0; i < Karakterler.Count; i++)
            {
                if (!Karakterler[i].activeInHierarchy)
                {

                    Karakterler[i].transform.position = new Vector3(VarisNoktasi.transform.position.x + (float)(random.NextDouble() / 5), VarisNoktasi.transform.position.y, VarisNoktasi.transform.position.z + (float)(random.NextDouble()));
                    Karakterler[i].SetActive(true);
                    OlusmaEfektiOlusturma(Karakterler[i].transform);
                    KarakterSayisi++;
                    eklenecekKarakterSayisi--;
                    break;
                }

            }

        }
        
    }

    public void Toplama(int sayi, int karakterSayisi)
    {

        while (sayi != 0)
        {
            for (int i = 0; i < Karakterler.Count; i++)
            {
                if (!Karakterler[i].activeInHierarchy)
                {

                    Karakterler[i].transform.position = new Vector3(VarisNoktasi.transform.position.x + (float)(random.NextDouble() / 5), VarisNoktasi.transform.position.y, VarisNoktasi.transform.position.z + (float)(random.NextDouble()));
                    Karakterler[i].SetActive(true);
                    OlusmaEfektiOlusturma(Karakterler[i].transform);
                    KarakterSayisi++;
                    sayi--;
                    break;
                }

            }

        }
    }

    public void Cikarma(int sayi, int karakterSayisi, Transform other)
    {
        int kalacakKarakter = karakterSayisi - sayi;
        if (sayi >= karakterSayisi)
        {
            int i = 0;
            while (KarakterSayisi != 1)
            {
                if (Karakterler[i].activeInHierarchy)
                {
                    YokOlmaEfektiOlusturma(other.transform, other.tag);
                    Karakterler[i].SetActive(false);
                    KarakterSayisi--;
                }

                i++;
            }
        }
            
        else
        {
            int i = 0;
            while (KarakterSayisi != kalacakKarakter)
            {
                if (Karakterler[i].activeInHierarchy)
                {
                    YokOlmaEfektiOlusturma(other.transform, other.tag);
                    Karakterler[i].SetActive(false);
                    KarakterSayisi--;
                    
                }
                i++;
                
            }
            
        }
    }
    
    public void Bolme(int sayi, int karakterSayisi, Transform other)
    {
        int kalacakKarakter = karakterSayisi / sayi;
        if (sayi >= karakterSayisi)
        {
            for (int i = 0; i < karakterSayisi - 1; i++)
            {
                YokOlmaEfektiOlusturma(other.transform, other.tag);
                Karakterler[i].SetActive(false);
                KarakterSayisi--;
            }
        }
        else
        {
            int i = 0;
            while (KarakterSayisi != kalacakKarakter)
            {
                if (Karakterler[i].activeInHierarchy)
                {
                    YokOlmaEfektiOlusturma(other.transform, other.tag);
                    Karakterler[i].SetActive(false); 
                    KarakterSayisi--;

                }
                i++;

            }
        }
    }

    public void YokOlmaEfektiOlusturma(Transform pozisyon, string tag)
    {
        foreach (GameObject item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                if(tag == "Engel" || tag == "Cikarma" || tag == "Bolme")
                {
                    item.transform.position = pozisyon.position;
                    item.SetActive(true);
                    break;
                }
                else if(tag == "Dusman")
                {
                    item.transform.position = pozisyon.position + Vector3.up *1.4f;
                    item.SetActive(true);
                    break;
                }
                
            }
        }
    }

    public void OlusmaEfektiOlusturma(Transform pozisyon)
    {
        foreach (GameObject item in OlusmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = pozisyon.position;
                item.SetActive(true);
                break;
            }
        }
    }
}
