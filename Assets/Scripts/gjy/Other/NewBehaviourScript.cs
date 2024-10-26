using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    void Start()
    {
        //渲染网格需要MeshRender和MeshFiler组件
        gameObject.AddComponent<MeshRenderer>();
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        //网格的创建
        Mesh mesh = new Mesh();
        //创建网格四个顶点
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0)
        };
        //定义网格三角形
        int[] tris = new int[6]
        {
            0,2,1,
            2,3,1,
        };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        meshFilter.mesh = mesh;
    }
}