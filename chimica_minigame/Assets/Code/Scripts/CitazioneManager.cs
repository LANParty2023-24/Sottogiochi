using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitazioniManager : MonoBehaviour
{
    public Color main;
    public Color lightMain;
    public TextMeshProUGUI text;

    private int currentCitazione=0;
    private string[] CITAZIONI={"\"La chimica è la poesia degli elementi.\"\nAlfred North Whitehead",
    "\"La chimica è una sorta di magia naturale; essa coinvolge processi che sono invisibili per l'occhio umano ma che hanno un impatto enorme sulla nostra vita.\"\nKathryn Schulz"};
    
    public int changeTime=5;
    private float currentTime=0;
    // Start is called before the first frame update
    void Start()
    {
        changeText();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+=Time.deltaTime;
        if(currentTime>=changeTime){
            currentTime=0;
            currentCitazione++;//errore di overflow, sarà fantastico se succede
            changeText();
        }
        FlashingText();
    }

    private void FlashingText(){
        text.color=Color.Lerp(main, lightMain, Mathf.PingPong(Time.time, 1));
    }

    private void changeText(){
        text.text=CITAZIONI[currentCitazione%CITAZIONI.Length];
    }
}
