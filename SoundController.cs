using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource sonido;
    public AudioClip clip1, clip2, clip3, clip4;

    //public Button button1, button2;

    void Start()
    {
        /*
        button1.onClick.AddListener(Sonido1);
        button2.onClick.AddListener(Sonido2);
        */

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }

    public void Sonido1()
    {
        sonido.PlayOneShot(clip1);
        //Debug.Log("SomeFunction1");
    }

    public void Sonido2()
    {
        sonido.PlayOneShot(clip2);
        //Debug.Log("SomeFunction2");
    }

    public void Sonido3()
    {
        sonido.PlayOneShot(clip3);
        //Debug.Log("SomeFunction2");
    }

}
