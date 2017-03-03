using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private Material quadMaterial;

    private int border = 5;

    private void Start ()
    {
        CreateGrid ();
    }

	public void GameOver (GameObject player)
    {
        Debug.Log (player.name + " loses!");
    }

    private void CreateGrid ()
    {
        Vector3 screenWorldSize = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0f));
        for (int x = (int)-screenWorldSize.x - border; x < (int)screenWorldSize.x + border; x++)
        {
            for (int y = (int)-screenWorldSize.y - border; y < (int)screenWorldSize.y + border; y++)
            {
                GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
                quad.GetComponent<MeshRenderer> ().material = quadMaterial;
                quad.transform.position = new Vector3 (x, y, 1f);
                quad.transform.localScale = Vector3.one * 0.975f;
                quad.transform.parent = transform;
            }
        }
    }

}
