using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    private bool isGameOver;
    public bool IsGameOver {
        get {
            return isGameOver;
        }
    }

    [SerializeField]
    private Text gameOverText;

    private BikeController[] players;
    private float gameoverDelay = 3f;

    protected override void Awake ()
    {
        base.Awake ();
        players = GameObject.FindObjectsOfType<BikeController> ();
        gameOverText.text = "";
    }

    private void Start ()
    {
        gameOverText.enabled = false;
    }

	public void GameOver (GameObject player)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            int loserNum = (int.Parse(player.name.Substring (player.name.Length - 1)));
            PlaySfxThroughGameController (SoundManager.Instance.GameOverSound);
            StartCoroutine (GameOverRoutine (loserNum));
        }
    }

    private IEnumerator GameOverRoutine (int loserNum)
    {
        foreach (BikeController bike in players)
        {
            bike.speed = 0;
            bike.rotSpeed = 0;
        }

        gameOverText.text = loserNum == 1 ? "Player 2 wins" : "Player 1 wins";
        gameOverText.enabled = true;
        StartCoroutine (SoundManager.Instance.ReduceMusicVolumeOverTime (gameoverDelay));
        yield return new WaitForSeconds (gameoverDelay);

        SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);

    }

	private void PlaySfxThroughGameController (AudioClip clip, float volMultiplier = 1.0f) {
		if (SoundManager.Instance.IsSfxEnabled && clip) {
			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(SoundManager.Instance.SfxVolume * volMultiplier, 0.05f, 1f));
		}
	}

}
