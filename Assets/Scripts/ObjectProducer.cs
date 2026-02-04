using System.Collections;
using UnityEngine;

public class ObjectProducer : MonoBehaviour
{
    public bool isCreatingObject;
    public float productionTime;
    public float removalTime;
    public GameObject objectToProduce;

    private void Awake()
    {
        StartCoroutine(ProduceObject());
    }
    private IEnumerator ProduceObject()
    {
        yield return new WaitForSeconds(productionTime);
        if (isCreatingObject)
        {
            GameObject NewObject = Instantiate(objectToProduce);
            yield return StartCoroutine(RemoveObject(NewObject));
        }
        else
        {
            objectToProduce.SetActive(true);
            yield return StartCoroutine(RemoveObject(objectToProduce));
        }
        StartCoroutine(ProduceObject());
    }
    private IEnumerator RemoveObject(GameObject RemovedObject)
    {
        yield return new WaitForSeconds(removalTime);
        if (isCreatingObject)
        {
            Destroy(RemovedObject);
        }
        else
        {
            RemovedObject.SetActive(false);
        }
    }
}
