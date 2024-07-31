
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    [SerializeField] float CharSpeed;
    public GameObject gameManager;
    GameManager manager;
    public bool SaldiriBasla;
    public GameObject OrtaNokta;
    public Slider slider;
    public GameObject Sinir;
    float ParmakPozX;
    void Start()
    {
        float fark = Vector3.Distance(transform.position, Sinir.transform.position);
        slider.maxValue = fark; 
        manager = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!SaldiriBasla)
        {
            slider.value = Vector3.Distance(transform.position, Sinir.transform.position);
            transform.Translate(Vector3.forward * CharSpeed * Time.deltaTime);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.x, 10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        ParmakPozX = touchPosition.x - transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        
                         transform.position = Vector3.Lerp(transform.position, new Vector3(touchPosition.x - ParmakPozX, transform.position.y, transform.position.z), 0.1f);
                        

      
                        //if (touchPosition.x - ParmakPozX > -1.45f && touchPosition.x - ParmakPozX < 1.45f)
                        //{
                        //    transform.position = Vector3.Lerp(transform.position, new Vector3(touchPosition.x - ParmakPozX,
                        //    transform.position.y, transform.position.z), 1f);
                        //}
                        break;
                }
            }

            if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
            {

                if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), 0.3f);
                }

                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), 0.3f);

                }
            }
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, OrtaNokta.transform.position, .01f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma"))
        {
            manager.Carpma(int.Parse(other.name), manager.KarakterSayisi); 
        }
        else if (other.CompareTag("Toplama"))
        {
            manager.Toplama(int.Parse(other.name), manager.KarakterSayisi);
        }
        else if (other.CompareTag("Cikarma"))
        {
            manager.Cikarma(int.Parse(other.name), manager.KarakterSayisi, other.transform);
        }
        else if (other.CompareTag("Bolme"))
        {
            manager.Bolme(int.Parse(other.name), manager.KarakterSayisi, other.transform);
        }

        else if (other.CompareTag("Sinir"))
        {
            SaldiriBasla = true;
            gameManager.GetComponent<GameManager>().SaldiriBasla = true;
        }


    }
}
