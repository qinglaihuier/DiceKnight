using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    int size = 1;

    int lifeTime = 1;

    Color color;

    private void OnEnable()
    {
        StartCoroutine(nameof(lifeCoroutine));
    }

    public void Initialize(int size, Color color, Vector3 position, int lifeTime = 1)
    {
        this.size = size;   

        this.color = color;

        transform.position = position;

        this.lifeTime = lifeTime;
    }
    IEnumerator lifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);

        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawCube(transform.position, Vector3.one * size);
    }
}
