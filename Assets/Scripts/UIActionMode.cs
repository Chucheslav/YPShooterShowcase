using System;
using UnityEngine;


public class UIActionMode : MonoBehaviour
{
    [SerializeField] private UIHealthBar healthBar;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private RectTransform reticle;
    [SerializeField] private HealthSo healthSo; //вот тут мы связали два игровых объекта через скриптовый
    [SerializeField] private SimpleEvent playerDeathEvent;
    
    private Aimer aimer;

    private void OnEnable()
    {
        // еще один способ связать объекты между собой. Довольно "хрупкий"
        aimer = FindObjectOfType<Aimer>(true);
        playerDeathEvent.Subscribe(ShowDeathScreen);
        gameOverScreen.SetActive(false);
    }

    private void OnDisable()
    {
        playerDeathEvent.Unsubscribe(ShowDeathScreen);
    }

    private void ShowDeathScreen()
    {
        gameOverScreen.SetActive(true);
        healthBar.gameObject.SetActive(false);
        reticle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(aimer)
            reticle.transform.position = aimer.ScreenPoint;
        
        //так как мы просто обращаеся к скриптовому объекту - даже обращение в апдейте безопасно и независимо.
        //Совсем правильно, конечно, сделать через события.
        healthBar.SetHealthBar(healthSo.GetFraction());
    }

    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}