using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GenerateGamePlay : MonoBehaviour
{
    public float speed;
    private float genInterval;
    private float timer = 0f;
    public GameObject obstacle;
    public GameObject crystal;
    public GameObject goblin;
    private float spawnPosition = 20f;
    void Start()
    {
        genInterval = 3f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= genInterval)
        {
            int generateType = Random.Range(0, 19);
            int direction = Random.Range(0, 2) * 2 - 1;
            timer = 0;
            genInterval = Random.Range(1, 3);
            if (generateType < 6)
            {
                GameObject obstacleClone = Instantiate(obstacle, new Vector3(spawnPosition, direction * 2.93f, 0), Quaternion.identity);
                obstacleClone.transform.localScale = new Vector3(0.25f, -1 * direction * 0.25f, 1f);
            }
            else if (generateType < 12)
            {
                GameObject crystalClone = Instantiate(crystal, new Vector3(spawnPosition, direction * 2.86f, 0), Quaternion.identity);
                crystalClone.transform.localScale = new Vector3(0.2f, -1 * direction * 0.2f, 1f);
            }
            else if (generateType < 13)
            {
                GameObject goblinClone = Instantiate(goblin, new Vector3(spawnPosition, direction * 3.13f, 0), Quaternion.identity);
                goblinClone.transform.localScale = new Vector3(0.2f, -1 * direction * 0.2f, 1f);
                goblinClone.GetComponent<Goblin>().position = -direction;
            }
            else
            {
                Boss();
            }
        }
    }
    void Boss()
    {
        GameManager.Instance.setPosition(transform.Find("Background").transform.position.x);
        GameManager.Instance.setPlayX(GameObject.Find("Player").transform.position.x);
        GameManager.Instance.setPlayY(GameObject.Find("Player").transform.position.y);
        SceneManager.LoadScene("transit");
    }
}
