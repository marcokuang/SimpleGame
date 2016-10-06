using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour {
    public Transform target;
    public float speed = 24.5f;
    public GameObject over;

    // Use this for initialization
    private GUIStyle gs = new GUIStyle();
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(target.position);
        //transform.Translate(Vector3.forward * speed* Time.deltaTime);
        //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        if(target != null)
        {
            float step = speed * Time.deltaTime;
            Vector3 newPos = target.position;
            newPos.y = 1.5f;
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        }
        else
        {
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene("map", LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT PLAYERRR!!!");
            Destroy(other.gameObject);
            
            //Application.LoadLevel(Application.loadedLevel);
            //over = GameObject.FindWithTag("Finish");
            //TextMesh theText = GameObject.Find("GameOverBoard").GetComponent<TextMesh>();
            //theText.gameObject.SetActive(true);
            over.SetActive(true);
            Destroy(gameObject);
            StartCoroutine(WaitGameOver(10.0f));

            SceneManager.LoadScene("map", LoadSceneMode.Single);

        }
    }

    IEnumerator WaitGameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield return new WaitForSeconds(3f);
    }
}
