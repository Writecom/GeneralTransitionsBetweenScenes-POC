using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance;
    
    //SerializeField kunne også være public,
    //det er bare en anden måde at lave et felt i Unity
    //vi kan drag and droppe vores GameObject
    [SerializeField] Animator transitionAnim;
    
    public void NextLevel2()
    {
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        //hvad fuck er en coroutine? https://docs.unity3d.com/Manual/Coroutines.html 
        //En coroutine bruges til at dele opgaver for en method ud over flere frames
        //Normalt, når en method kaldes, skal den klare hele sin operation over 1 fram
        //Men hvis man, som i vores tilfælde, har flere animationer der skal spille
        //i en method, kan vi jo ikke have at den samtidig loader vores næste scene, 
        //inden vores animation er færdig. Derfor bruger vi coroutine. Tror jeg.

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        //Animatoren sætter den trigger vi har lavet til End, så den loader transitioner fra Start anim til End Anim, og kun spiller 1 gang
        transitionAnim.SetTrigger("End");
        
        //yield return skal være på tidspunkt i en Coroutine, og kan også have værdi null
        //I dette tilfælde, bruger vi det til at lave en 1 sek pause mellem slut anim og resten af koden som så er loade ny scene og starte start anim
        yield return new WaitForSeconds(1);
        
        //LoadSceneAsync method, loader scene asynkront i baggrunden, for at undgå game freeze, da LoadScene fryser scenen til den har loadet næste scene
        //For parametre skal den bruge en int der svarer til build indekset, som kan findes 'File' under 'Build Settings' som er oppe i venstre hjørne. Det er samme sted man tilføjer sine scener til sin build
        //Denne int bliver så hentet gennem vores method GetActiveScene og beder om indekset, og så lægger vi 1 til, for at den skal loade næste level
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        
        //Når den har loadet vores level, skal den kører start anim
        transitionAnim.SetTrigger("Start");
    }

    //Nedenstående kode er til at passere en instans af et gameObject mellem scener
    //Er forsøgt at blive brugt, man har ikke fundet helt ud af det endnu til vores formål
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
