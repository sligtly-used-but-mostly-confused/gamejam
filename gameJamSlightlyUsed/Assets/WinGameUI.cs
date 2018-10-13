using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour {

    public Text WinGameText;

	public void WinGame(PlayerController player)
    {
        WinGameText.text = $"PLAYER {player.name} WON!";
    }
}
