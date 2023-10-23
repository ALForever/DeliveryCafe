using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoveed;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpownAmount;
    private int platesSpownAmountMax = 4;



    private void Update() {
        if (KitchenGameManager.Instance.IsGamePlaying()) {
            spawnPlateTimer += Time.deltaTime;
        }

        if (spawnPlateTimer > spawnPlateTimerMax ) {
            spawnPlateTimer = 0f;
            
            if (platesSpownAmount < platesSpownAmountMax) {
                platesSpownAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }


    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            //Player has empty handed
            if (platesSpownAmount > 0) { 
                //There's at least one plate here
                platesSpownAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoveed?.Invoke(this, EventArgs.Empty);
            }


        }
    }

}
