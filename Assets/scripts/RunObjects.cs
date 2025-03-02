using UnityEngine;

public class RunObjects : MonoBehaviour
{
    private GenerateGamePlay generateGamePlay;
    void Start()
    {
        generateGamePlay = GameObject.Find("GenerateGamePlay").GetComponent<GenerateGamePlay>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-generateGamePlay.speed, 0, 0) * Time.deltaTime;
        if (transform.position.x <= -35f)
        {
            Destroy(gameObject);
        }
    }
}
