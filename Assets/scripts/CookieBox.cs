using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CookieBox : MonoBehaviour
{
    public GameObject[] cookiePrefabs;
    public int cookieCount;

    private RectTransform rectTransform;
    public RectTransform spawnArea;
    private List<GameObject> spawnedCookies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnAfterLayout());
    }   

IEnumerator SpawnAfterLayout()
{
    yield return null;
    rectTransform = spawnArea;
}

    public void SpawnCookies(int count)
{
    ClearCookies();

    List<Vector2> usedPositions = new List<Vector2>();
    float cookieSize = 70f;
    int maxAttempts = 1000;

    for (int i = 0; i < count; i++)
    {
        float halfWidth = rectTransform.rect.width / 2;
        float halfHeight = rectTransform.rect.height / 2;

        Vector2 newPosition;
        int attempts = 0;
        bool validPosition = false;

        do
        {
            newPosition = new Vector2(
                Random.Range(-halfWidth + cookieSize, halfWidth - cookieSize),
                Random.Range(-halfHeight + cookieSize, halfHeight - cookieSize)
            );

            validPosition = true;
            foreach (Vector2 usedPosition in usedPositions)
            {
                if (Vector2.Distance(newPosition, usedPosition) < cookieSize)
                {
                    validPosition = false;
                    break;
                }
            }

            attempts++;
        } while (!validPosition && attempts < maxAttempts);

        usedPositions.Add(newPosition);
        GameObject randomPrefab = cookiePrefabs[Random.Range(0, cookiePrefabs.Length)];
        GameObject cookie = Instantiate(randomPrefab, spawnArea.transform);
        cookie.GetComponent<RectTransform>().anchoredPosition = newPosition;
        spawnedCookies.Add(cookie);
    }
}

    public void ClearCookies()
    {
        foreach (GameObject cookie in spawnedCookies)
        {
            Destroy(cookie);
        }
        spawnedCookies.Clear();
    }
}