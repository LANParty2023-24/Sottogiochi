using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLiquid_Manager_Script : MonoBehaviour
{
    public Color color;

    public GameObject[] subliquids=new GameObject[3];

    void Start()
    {
        ChangeColor(color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color c){
        this.color=c;
        foreach(GameObject l in subliquids){
            l.GetComponent<SpriteRenderer>().color=this.color;
        }
    }


}
