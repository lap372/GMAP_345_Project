using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//create SO by going here before class  name
[CreateAssetMenu()]

public class KitchenObjectSO : ScriptableObject
{

    //read only data for the SO's,never write to them says codde monkey...
    public Transform prefab;
    public Sprite sprite;
    public string objectName;




}
