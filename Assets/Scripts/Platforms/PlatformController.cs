using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    float minHeight;
    [SerializeField]
    float maxHeight;
    [SerializeField]
    GameObject[] platformPrefabs = new GameObject[1];
    [SerializeField]
    GameObject[] specialPlatformPrefabs = new GameObject[1];
    [SerializeField]
    float specialPlatformPercentage;
    [SerializeField]
    int maxPlatormsToGenerate = 5;
    int platformCounter=0;
    [SerializeField]
    List<GameObject> generatedPlatforms;
    [SerializeField]
    int generateStep = 100;

    List<int> specialPlatformIndex = new List<int>();
    int nextGenerate = 0;
    private Vector3 screenBottomLeft;
    private Vector3 screenTopRight;
    private bool TriggerGeneratePlastorm = true;
    private LevelManager levelManager;
    void Start()
    {
        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        platformCounter = maxPlatormsToGenerate;
        levelManager = LevelManager.Instance;
        nextGenerate += generateStep;

        SpecialPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerGeneratePlastorm)
        {
            GeneratePlatforms(generatedPlatforms[0]);
        }
        if (levelManager.GetPlayerScore() > nextGenerate)
        {
            TriggerGeneratePlatorm();
            nextGenerate += generateStep;
            SpecialPlatform();
        }
    }
    // TODO: add a max number of tries
    void GeneratePlatforms(GameObject first)
    {
        Vector2 newPosition = GetRandomPosition(generatedPlatforms[generatedPlatforms.Count - 1].transform.position);

        if (platformCounter > 0)
        {
            if (!CheckIfOverlap(newPosition))
            {
                if (specialPlatformIndex.Contains(platformCounter))//Generate Special Platform Random
                {
                    Debug.Log("Generated Special Platform");
                    var index = Random.Range(0, specialPlatformPrefabs.Length);
                    generatedPlatforms.Add(Instantiate(specialPlatformPrefabs[index], this.transform, true));
                    generatedPlatforms[generatedPlatforms.Count - 1].transform.position = newPosition;
                }
                else // Generate normal platform
                {
                    Debug.Log("Generated Normal Platform");
                    generatedPlatforms.Add(Instantiate(platformPrefabs[0], this.transform, true));
                    generatedPlatforms[generatedPlatforms.Count - 1].transform.position = newPosition;
                }
                platformCounter--;
            }
        }
        else
        {
            TriggerGeneratePlastorm = false;
        }
    }
    public void TriggerGeneratePlatorm()
    {
        if (!TriggerGeneratePlastorm)
        {
            TriggerGeneratePlastorm = true;
            platformCounter = maxPlatormsToGenerate;
        }
    }
    Vector2 GetRandomPosition(Vector2 pos)
    {
        var minMax = Random.Range(minHeight, maxHeight);

        return new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), minMax + pos.y);
    }
    //TODO: Check if outside of screen
    bool CheckIfOverlap(Vector2 position)
    {
        int layerMask = 1 <<9; // Only hit boxes in overlapCheck layer
        Vector2 overlapTestBoxScale = new Vector3(0.7f*3, 0.15f*3); //Scale of OverlapCheckBox
        Collider2D[] collidersInsideOverlapBox = new Collider2D[1];
        int numberOfCollidersFound = Physics2D.OverlapBoxNonAlloc(position, overlapTestBoxScale, 0.0f,collidersInsideOverlapBox, layerMask);

        //Debug draw box
        Debug.DrawRay(new Vector3(position.x - overlapTestBoxScale.x / 2, position.y + overlapTestBoxScale.y / 2, 0.0f), transform.TransformDirection(Vector3.down) * overlapTestBoxScale.y, Color.red);
        Debug.DrawRay(new Vector3(position.x + overlapTestBoxScale.x / 2, position.y + overlapTestBoxScale.y / 2, 0.0f), transform.TransformDirection(Vector3.down) * overlapTestBoxScale.y, Color.red);
        Debug.DrawRay(new Vector3(position.x - overlapTestBoxScale.x/2, position.y + overlapTestBoxScale.y/ 2, 0.0f), transform.TransformDirection(Vector3.right) * overlapTestBoxScale.x, Color.red);
        Debug.DrawRay(new Vector3(position.x - overlapTestBoxScale.x / 2, position.y - overlapTestBoxScale.y / 2, 0.0f), transform.TransformDirection(Vector3.right) * overlapTestBoxScale.x, Color.red);

        if (numberOfCollidersFound == 0)
        {
            //Check if outside of screen
            if (CheckIfInsideOfScreenBounds(overlapTestBoxScale,position))
            {
                Debug.Log("Platform Generator CheckIfOverlap Passed");
                Debug.DrawRay(new Vector3(position.x, position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
                return false;
            }
            Debug.Log("Platform Generator CheckIfInsideOfScreenBounds Failed");
            return true;
        }
        else
        {
            Debug.Log("Platform Generator CheckIfOverlap Failed");
            Debug.DrawRay(new Vector3(position.x, position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return true;
        }
    }
    bool CheckIfInsideOfScreenBounds(Vector2 overlapTestBox,Vector2 position)
    {
        if(position.x + overlapTestBox.x/2 > screenTopRight.x || position.x - overlapTestBox.x / 2 < screenBottomLeft.x)
            return false;
        return true;
    }
    void SpecialPlatform()
    {
        if (specialPlatformPrefabs.Length > 0)
        {
            int p = (int)(maxPlatormsToGenerate * specialPlatformPercentage);
            specialPlatformIndex.Clear();
            for (int i = 0; i < p; i++)
            {
                var index = Random.Range(0, maxPlatormsToGenerate);
                while(specialPlatformIndex.Contains(index))
                    index = Random.Range(0, maxPlatormsToGenerate);
                specialPlatformIndex.Add(index);
            }
        }
    }
}
