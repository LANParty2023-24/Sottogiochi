using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProvettaManager : MonoBehaviour
{
    public bool Empty;
    public int number_of_liquids;
    private bool isMoving;
    private Color TransparentColor;
    private GameObject[] liquids;
    private Vector3 initialPosition;

    void Start() {
        isMoving=false;
        initialPosition=gameObject.transform.position;
        liquids=new GameObject[number_of_liquids];
        this.TransparentColor=new Color(1,1,1,0);

        InstantiateLiquids();

        if(Empty)
            ChangeAllToTransparent();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //mi assicuro che la provetta this sia in collisione con un'altra provetta
        if(collision.gameObject.layer!=6 || isMoving) return;
        
        //provetta che collide
        ProvettaManager provettaCollisione = collision.gameObject.GetComponent<ProvettaManager>();
        int collision_topLiquidIndex = provettaCollisione.GetFirstVisibleLiquid();

        //provetta this
        int this_topLiquidIndex = GetFirstVisibleLiquid();

        //Debug.Log(collision.gameObject.name+" COLLISION\ntop-liquid-index: "+collision_topLiquidIndex+"   top-liquid-color: ");
        //Debug.Log(gameObject.name+" THIS\ntop-liquid-index: "+this_topLiquidIndex+"    top-liquid-color: ");

        //provetta this è piena(topLiquidIndex=0) o la provetta collision è vuota(topLiquidIndex=-1), allora alcun liquido può essere scambiato
        if(this_topLiquidIndex==0 || collision_topLiquidIndex==-1) return;

        //se provetta this vuota(=-1) allora cambio il colore dell'ultimo, altrimenti del primo vuoto(topLiquidIndex-1)
        liquids[(this_topLiquidIndex==-1 ? number_of_liquids-1:this_topLiquidIndex-1)].GetComponent<SpriteRenderer>().color=provettaCollisione.GetLiquids()[collision_topLiquidIndex].GetComponent<SpriteRenderer>().color;
        //rendo invisibile(svuoto) il liquido dalla provetta collision
        provettaCollisione.GetLiquids()[collision_topLiquidIndex].GetComponent<SpriteRenderer>().color=TransparentColor;
    }

    private void OnMouseDrag(){
        isMoving=true;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z=0;
        gameObject.transform.position=worldMousePosition;
        //Debug.Log(gameObject.transform.position);
    }

    private void OnMouseUp()
    {
        isMoving=false;
        gameObject.transform.position = initialPosition;//reset position
    }


    public void ChangeAllToTransparent(){
        for(int i=0; i<number_of_liquids; i++){
            liquids[i].GetComponent<SpriteRenderer>().color=TransparentColor;
        }
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

    public GameObject[] GetLiquids(){
        return liquids;
    }

    public Color[] GetColors(){
        Color[] colors = new Color[number_of_liquids];
        for(int i=0; i<number_of_liquids; i++){
            colors[i]=liquids[i].GetComponent<SpriteRenderer>().color;
        }
        return colors;
    }

    public int GetFirstVisibleLiquid(){
        int firstVisible=-1;
        for(int i=0;i<number_of_liquids; i++){
            if(liquids[i].GetComponent<SpriteRenderer>().color.a!=0){
                firstVisible=i;
                break;
            }
        }

        return firstVisible;
    }

    public bool isEmpty(){
        return GetFirstVisibleLiquid()==-1 ? true : false;
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