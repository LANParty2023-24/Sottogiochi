using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitazioniManager : MonoBehaviour
{
    public float dilate;
    public Color main;
    public Color lightMain;
    public TextMeshProUGUI text;
    public float WPS=3; //Words Per Second(reading velocity)
    private string[] QUOTES={
        "\"La chimica è la poesia degli elementi.\"\nAlfred North Whitehead",
        "\"La chimica è una sorta di magia naturale; essa coinvolge processi che sono invisibili per l'occhio umano ma che hanno un impatto enorme sulla nostra vita.\"\nKathryn Schulz",
        "\"Nulla si crea, nulla si distrugge, tutto si trasforma.\"\nAntoine-Laurent Lavoisier"
    };
    
    private float[] QUOTES_READING_TIME;
    private int currentQuote=0;
    private float currentTime=0;

    // Start is called before the first frame update
    void Start()
    {
        InstatiateQuoteReadingTimes();
        changeText();

        //(int)getTotalReadingTime()*2+3 --> min time for reading all quotes twice + a little offset 
        GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene_Script>().SetSceneLivingSeconds((int)getTotalReadingTime()*2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene_Script>().changeScene();
        }

        currentTime+=Time.deltaTime;
        if(currentTime>=QUOTES_READING_TIME[currentQuote]){
            currentTime=0;
            currentQuote=(currentQuote+1)%QUOTES.Length;
            changeText();
        }
        //this.GetComponent<TMPro.TextMeshProUGUI>().fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_FaceDilate, dilate);
        FlashingText();
    }

    private void FlashingText(){
        text.color=Color.Lerp(main, lightMain, Mathf.PingPong(Time.time, 1));
    }

    private void changeText(){
        text.text=QUOTES[currentQuote];
    }

    private void InstatiateQuoteReadingTimes(){
        QUOTES_READING_TIME=new float[QUOTES.Length];
        for(int i=0; i<QUOTES.Length; i++){
            QUOTES_READING_TIME[i]=QUOTES[i].Split(" ").Length/WPS;
            Debug.Log(QUOTES_READING_TIME[i]);
        }
    }

    private float getTotalReadingTime(){
        float total=0;
        foreach(float t in QUOTES_READING_TIME)
            total+=t;
        
        return total;
    }
}
