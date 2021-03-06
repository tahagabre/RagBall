﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ROTHManager : MonoBehaviour
{
    public int[] scores;
    [SerializeField] private Player[] players;
    [SerializeField] private List<GameObject> UILocations;
    [SerializeField] private GameObject UIPrefab;
    [SerializeField] private GameObject canvasParent;
    [SerializeField] public GameObject endCanv;

    [SerializeField] private Material[] mats;

    private GameObject[] UIS;

    public delegate void Score(int playerNum, int scoreValue);

    public event Score OnScore;

    // Start is called before the first frame update
    void Start()
    {
        if (Game.Instance == null) return;
        StartCoroutine("WaitForStart");

        // Stop any incoming music (BackGround Music plays automatically)
        Game.Instance.Music.StopAudio();
    }
    IEnumerator WaitForStart()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("I get my start called");
        OnScore += addScore;
        ActionMapEvent.InGameplay?.Invoke();
        players = FindObjectsOfType<Player>();
        Player[] temp = new Player[players.Length];
        UIS = new GameObject[players.Length];
        scores = new int[players.Length];
        UILocations = canvasParent.GetComponent<StaminaUI>().getLocations();

        foreach (Player p in players)
        {
            int index = p.playerNumber;
            scores[index] = 0;
            temp[p.playerNumber] = p;
            p.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = mats[index];
            //instantiates UIS based on number of players and assigns to the locations //NOTE: WILL THROW ERROR IF MORE PLAYERS THAN STAMINA POSITIONS
            //UIS[index] = Instantiate(UIPrefab, UILocations[index].transform.position, Quaternion.identity, canvasParent.transform);

        }
        players = temp;
    }
    public void OnDestroy()
    {
        OnScore -= addScore;
    }
    public void addScore(int pNum, int value)
    {
        scores[pNum] += value;
        players[pNum].updateScore(scores[pNum]);
        //UIS[pNum].transform.GetComponentInChildren<TextMeshProUGUI>().text = scores[pNum].ToString();
    }
}
