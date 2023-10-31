using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Animations;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    [SerializeField] Texture2D heightMap;
    [SerializeField] Vector3[] vertices;
    int[] triangles;

    [SerializeField] int rowX;
    [SerializeField] int columnZ;
    [SerializeField] float height;
    int texSize;

    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        vertices = new Vector3[(rowX + 1) * (columnZ + 1)];
        texSize = heightMap.width * heightMap.height;

        int i = 0;
        for (int x = 0; x < rowX; x++)
        {
            for (int z = 0; z < columnZ; z++)
            {

                //if pixel of the tetxture is greater than the vertices

                //divide the pixel by the vertices

                //then the answer of thhat will be a for loop where each vertices will contain a result of texture pixel divided by vertices
                // and that will be the height map

                Color pixel = heightMap.GetPixel(Mathf.FloorToInt(z * (heightMap.width - 1) / rowX), Mathf.FloorToInt(x * (heightMap.height - 1) / columnZ));

                //Color pixel = heightMap.GetPixel(z, x);

                vertices[i] = new Vector3(z, pixel.r * height, x);
                i++;
            }
        }

        meshFilter.mesh.vertices = vertices;

        triangles = new int[rowX * columnZ * 6];
        int cv = 0;
        int tris = 0;
        for (int z = 0; z < columnZ; z++)
        {
            for (int x = 0; x < rowX; x++)
            {
                triangles[tris + 0] = cv;
                triangles[tris + 1] = cv + columnZ;
                triangles[tris + 2] = cv + columnZ + 1;

                triangles[tris + 3] = cv + columnZ + 1;
                triangles[tris + 4] = cv + 1;
                triangles[tris + 5] = cv;
                cv++;
                tris += 6;
            }
        }

        meshFilter.mesh.triangles = triangles;

        // triangles = new int[] { cv, cv + 1, cv + columnZ + 1, cv + columnZ + 1, cv + columnZ, cv };

        // meshFilter.mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        //meshFilter.mesh.triangles = triangles;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
