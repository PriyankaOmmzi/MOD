using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public void FadeScreen()
    {

        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().Play("Fade");
       
    }

    public void LoadNexScreen()
    {
        Application.LoadLevel(1);
    }

  
}
