using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public void GameOver (GameObject player)
    {
        Debug.Log (player.name + " loses!");
    }

}
