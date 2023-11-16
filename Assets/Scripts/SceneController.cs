using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance;
    
    //SerializeField kunne ogs� v�re public,
    //det er bare en anden m�de at lave et felt i Unity
    //vi kan drag and droppe vores GameObject
    [SerializeField] Animator transitionAnim;
    
    public void NextLevel2()
    {
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        //hvad fuck er en coroutine? https://docs.unity3d.com/Manual/Coroutines.html 
        //En coroutine bruges til at dele opgaver for en method ud over flere frames
        //Normalt, n�r en method kaldes, skal den klare hele sin operation over 1 fram
        //Men hvis man, som i vores tilf�lde, har flere animationer der skal spille
        //i en method, kan vi jo ikke have at den samtidig loader vores n�ste scene, 
        //inden vores animation er f�rdig. Derfor bruger vi coroutine. Tror jeg.

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        //Animatoren s�tter den trigger vi har lavet til End, s� den loader transitioner fra Start anim til End Anim, og kun spiller 1 gang
        transitionAnim.SetTrigger("End");
        
        //yield return skal v�re p� tidspunkt i en Coroutine, og kan ogs� have v�rdi null
        //I dette tilf�lde, bruger vi det til at lave en 1 sek pause mellem slut anim og resten af koden som s� er loade ny scene og starte start anim
        yield return new WaitForSeconds(1);
        
        //LoadSceneAsync method, loader scene asynkront i baggrunden, for at undg� game freeze, da LoadScene fryser scenen til den har loadet n�ste scene
        //For parametre skal den bruge en int der svarer til build indekset, som kan findes 'File' under 'Build Settings' som er oppe i venstre hj�rne. Det er samme sted man tilf�jer sine scener til sin build
        //Denne int bliver s� hentet gennem vores method GetActiveScene og beder om indekset, og s� l�gger vi 1 til, for at den skal loade n�ste level
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        
        //N�r den har loadet vores level, skal den k�rer start anim
        transitionAnim.SetTrigger("Start");
    }

    //Nedenst�ende kode er til at passere en instans af et gameObject mellem scener
    //Er fors�gt at blive brugt, man har ikke fundet helt ud af det endnu til vores form�l
    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */
}
