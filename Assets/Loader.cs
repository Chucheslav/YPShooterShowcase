using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject persistent;

    void Start()
    {
        SceneManager.LoadScene("MobsNavigationScene");
        // AsyncOperation op =SceneManager.LoadSceneAsync("MobsNavigationScene", LoadSceneMode.Additive);
        // StartCoroutine(WaitForLoad(op));
        
        DontDestroyOnLoad(persistent);
    }

    // private IEnumerator WaitForLoad(AsyncOperation op)
    // {
    //     while (!op.isDone) yield return null;
    //     SceneManager.SetActiveScene(SceneManager.GetSceneByName("MobsNavigationScene"));
    //     var t = SceneManager.UnloadSceneAsync("SampleScene");
    // }
    
}
