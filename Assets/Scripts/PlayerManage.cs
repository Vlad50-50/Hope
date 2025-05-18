using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class PlayerManage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private int maxRound;
    private int currRound;

    private Transform parentCheck;
    private int checkpointCouunt;
    private int checkpointLayer;
    private int checkpointsLast;
    private int currCheckpoint;

    private void UpdateUI(){
        roundText.text = $"CUBE: {currRound}/{maxRound}";
    }

    private void Awake(){
        currRound = 1;
        parentCheck = GameObject.Find("ChecPoints").transform;
        checkpointCouunt= parentCheck.childCount;
        checkpointLayer = LayerMask.NameToLayer("CheckPoints");
        UpdateUI();
    }

    void NextCUBE(){
        currCheckpoint = 1;
        currRound++;
        if(currRound >= maxRound){
            //game over
            Debug.Log("qwerty!");
        }
        UpdateUI();
    }

    private void OnTriggerEnter(Collider ob){
        if(ob.gameObject.layer == checkpointLayer){
            if(int.Parse(ob.gameObject.name) == 0 && currCheckpoint == checkpointCouunt){
                NextCUBE();
            }
            else  if(int.Parse(ob.gameObject.name) == currCheckpoint){
                currCheckpoint++;
                Debug.Log(currCheckpoint);
            }
        }
    }
}