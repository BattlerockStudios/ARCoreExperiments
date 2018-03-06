﻿
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon;

public class PlayerManager : PunBehaviour
{
    public string playerName;
    public Color playerColor;
    public int playerScore;


    // Use this for initialization
    public void Start()
    {
        playerName = PhotonNetwork.playerName;
        var col = (Vector3)PhotonNetwork.player.CustomProperties["PlayerColor"];
        playerColor = new Color(col.x, col.y, col.z, 1);

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if(PhotonNetwork.playerList[i].ID == photonView.owner.ID)
            {
                playerName = photonView.owner.NickName;

                var col = (Vector3)PhotonNetwork.player.CustomProperties[playerName];
                playerColor = new Color(col.x, col.y, col.z, 1);

                //set the Game objects underneath the Player like the paddle to the players color
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                {
                    r.material.color = playerColor;
                }
            }
        }  

        //for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
        //{
        //    var otherColor = (Vector3)PhotonNetwork.otherPlayers[i].CustomProperties["PlayerColor"];
        //    playerColor = new Color(otherColor.x, otherColor.y, otherColor.z, 1);

        //    var photonViews = GameObject.FindObjectsOfType<PhotonView>();
        //    for (int j = 0; j < photonViews.Length; j++)
        //    {
        //        Debug.Log("<Color=red>OtherPlayer ID: </Color>" + PhotonNetwork.otherPlayers[i].ID);
        //        Debug.Log("<Color=green>PhotonView Ownr ID: </Color>" + PhotonNetwork.otherPlayers[i].ID);
        //        if (photonViews[j].ownerId == PhotonNetwork.otherPlayers[i].ID)
        //        {
        //            Renderer[] renderers = photonViews[j].GetComponentsInChildren<Renderer>();
        //            //set the Game objects underneath the Player like the paddle to the players color
        //            foreach (Renderer r in renderers)
        //            {
        //                r.material.color = playerColor;
        //            }
        //        }
        //    }
        //}

        //reset players score to zero
        playerScore = 0;
        Hashtable setPlayerScore = new Hashtable() { { "PlayerScore", playerScore } };
        PhotonNetwork.player.SetCustomProperties(setPlayerScore);
    }

    /// <summary>
    /// up the players score when they get the puck in the opponents goal
    /// </summary>
    public void AddScore()
    {
        playerScore++;
        SetCustomProperty(new Hashtable() { { "PlayerScore", playerScore } });
    }

    /// <summary>
    /// Reduce the players score for when they score on themselves
    /// </summary>
    public void ReduceScore()
    {
        if (playerScore > 0)
        {
            playerScore--;
            SetCustomProperty(new Hashtable() { { "PlayerScore", playerScore } });
        }
    }

    /// <summary>
    /// Apply a custon property it's new value to the player
    /// </summary>
    /// <param name="value"></param> Hashtable of { "PropertyName", propertyValue }
    private void SetCustomProperty(Hashtable value)
    {
        PhotonNetwork.player.SetCustomProperties(value);
    }
}
