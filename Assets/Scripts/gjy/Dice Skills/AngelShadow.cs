using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelShadow : MonoBehaviour
{
    float lifeCycleSize = 1;

    // Start is called before the first frame update
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Initialize(float lifeCycleSize, Vector2 pos)
    {
        this.lifeCycleSize = lifeCycleSize;

        transform.position = pos;

        StartCoroutine(nameof(lifeCoroutine));
    }
    IEnumerator lifeCoroutine()
    {
        yield return new WaitForSeconds(lifeCycleSize);

        ObjectPool.GetInstance().PushGameObject(gameObject);

        Debug.Log("ÏûÊ§");
        Debug.Log(lifeCycleSize);
    }
}
