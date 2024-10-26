using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    void Start()
    {
        //��Ⱦ������ҪMeshRender��MeshFiler���
        gameObject.AddComponent<MeshRenderer>();
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        //����Ĵ���
        Mesh mesh = new Mesh();
        //���������ĸ�����
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0)
        };
        //��������������
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