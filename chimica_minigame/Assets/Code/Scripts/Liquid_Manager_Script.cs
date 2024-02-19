using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Liquid_Manager_Script : MonoBehaviour
{
    public bool isEmpty;
    public int number_of_liquids;
    private Color TransparentColor;
    private GameObject[] liquids;

    void Start() {
        liquids=new GameObject[number_of_liquids];
        this.TransparentColor=new Color(1,1,1,0);

        InstantiateLiquids();
        foreach(GameObject l in liquids){
            Debug.Log(l.name);
        }

        if(isEmpty)
            ChangeAllToTransparent();
    }

    public void ChangeAllToTransparent(){
        for(int i=0; i<number_of_liquids-1; i++){
            liquids[i].GetComponent<SpriteRenderer>().color=TransparentColor;
        }

        liquids[liquids.GetLength(0)-1].GetComponent<LastLiquid_Manager_Script>().ChangeColor(TransparentColor);
    }

    //forse tornerà utile
    public void ChangeColorRecursive(Color new_color, int current_index){
        if(current_index==liquids.GetLength(0)){
            liquids[current_index-1].GetComponent<SpriteRenderer>().color=new_color;
            return;
        }
    
        Color oldColor = liquids[current_index-1].GetComponent<SpriteRenderer>().color;
        //Debug.Log(oldColor.ToString());

        liquids[current_index-1].GetComponent<SpriteRenderer>().color=new_color;

        //colore precedente uguale colore attuale
        if(oldColor.Equals(liquids[current_index].GetComponent<SpriteRenderer>().color)) 
            ChangeColorRecursive(new_color, current_index+1);
    }

    private void InstantiateLiquids(){
        Transform parentTransform = gameObject.transform;
        int i=0;

        foreach (Transform child in parentTransform)
        {
            if(!child.name.ToLower().Contains("liquid"))
                continue;

            liquids[i]=child.gameObject;
            i++;
            //Debug.Log("Child Name: " + child.name);
        }
    }
}