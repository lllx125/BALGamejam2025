using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
public class BGTransit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject bg1;
    private GameObject bg2;
    void Start()
    {
        bg1 = transform.Find("Background").gameObject;
        bg2 = transform.Find("Background 2").gameObject;

        bg1.transform.position = new Vector3(GameManager.Instance.position, 0, 0);
        if (GameManager.Instance.position > 0)
        {
            bg2.transform.position = new Vector3(GameManager.Instance.position - 38.35f, 0, 0);
        }
        else
        {
            bg2.transform.position = new Vector3(GameManager.Instance.position + 38.35f, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(bg1.transform.position.x / Time.deltaTime) < 5 || Mathf.Abs((bg1.transform.position.x + 38.35f) / Time.deltaTime) < 5 || bg1.transform.position.x + 38.35f * 2 < 0)
        {
            SceneManager.LoadScene("boss");
        }
        transform.position += new Vector3(-5, 0, 0) * Time.deltaTime;
    }
}
