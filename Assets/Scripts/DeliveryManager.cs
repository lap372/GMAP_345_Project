using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }


    [SerializeField] private RecipeListSO recipeListSO;


    
    private List<RecipeSO> waitingRecipeSOList;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax =  4f;
    private int waitingRecipesMax = 4;


    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>(); //initilize list
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f )
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            //spawn recipe
            if(waitingRecipeSOList.Count < waitingRecipesMax ) 
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
            


        }
    }



    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; ++i) 
        { 
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];


            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has same nuber of ingredients, as a first check

                bool plateContentsMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycling through all ingredients in recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingredients in plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //player delivered correct recipe
                    Debug.Log("player delivered correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //no matches found
        //player did not deliver a correct reccipe
        Debug.Log("player did not deliver correct recipe");

    }


}
