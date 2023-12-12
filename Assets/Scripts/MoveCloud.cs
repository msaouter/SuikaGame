using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCloud : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minXPos = -2.6f;
    [SerializeField] private float maxXPos = 2.6f;

    public FruitsList[] fruitPrefabs;

    [SerializeField] private GameObject nextFruit;
    [SerializeField] private int nextFruitIndex;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int currentScore = 0;

    private bool canDropFruit = true;

    #region Singleton
    public static MoveCloud instance;

    private void Awake()
    {
        instance = this; 
    }

    #endregion

    private void Start()
    {
        LoadNextFruit();   
    }

    // Update is called once per frame
    void Update()
    {
        //Left / Right controls
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minXPos)
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxXPos)
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)/* && canDropFruit*/)
        {
            canDropFruit = false;
            StartCoroutine(ResetDropTimer());

            GameObject newFruit = Instantiate(nextFruit);
            newFruit.transform.position = transform.position;
         
            Fruit newFruitScript = newFruit.GetComponent<Fruit>();
            newFruit.GetComponent<Fruit>().fruitIndex = nextFruitIndex;

            LoadNextFruit();
        }
    }

    void LoadNextFruit()
    {
        nextFruitIndex = Random.Range(0, 4);
        nextFruit = fruitPrefabs[nextFruitIndex].prefab;

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject nextFruitPreview =  Instantiate(nextFruit, transform);
        nextFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
        nextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
        nextFruitPreview.transform.localPosition = Vector3.zero;

    }

    public void IncreaseScore(int value)
    {
        print(this.name);
        currentScore += value;
        scoreText.SetText(currentScore.ToString());
    }

   IEnumerator ResetDropTimer() 
    {
        yield return new WaitForSeconds(1f);
        canDropFruit = true;
    }
}

[System.Serializable]
public class FruitsList
{
    public GameObject prefab;
    public int points;
}
