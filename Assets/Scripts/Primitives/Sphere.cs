using System;
using UnityEngine;


public class Sphere : Primitive
{
    [SerializeField] private PrimitiveParameterEditor _radiusEditor, _segmentsEditor;
    private float _radius = 1f;
    private int _segments = 16;

    protected override void AddListenersToParameterEditors()
    {
        _radiusEditor.AddListener((value =>
        {
            _radius = value;
            GenerateMesh();
        }));
        _segmentsEditor.AddListener((value =>
        {
            _segments = (int) value;
            GenerateMesh();
        }));
    }

    protected override void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        _meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[(_segments + 1) * (_segments + 1)];
        Vector3[] normals = new Vector3[vertices.Length];
        int[] triangles = new int[_segments * _segments * 6];
        Vector2[] uvs = new Vector2[vertices.Length];

        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i <= _segments; i++)
        {
            for (int j = 0; j <= _segments; j++)
            {
                float theta = j * Mathf.PI * 2 / _segments;
                float phi = i * Mathf.PI / _segments;

                float x = Mathf.Sin(phi) * Mathf.Cos(theta) * _radius;
                float y = Mathf.Sin(phi) * Mathf.Sin(theta) * _radius;
                float z = Mathf.Cos(phi) * _radius;

                Vector3 vertex = new Vector3(x, y, z);
                vertices[vertIndex] = vertex;
                
                normals[vertIndex] = vertex.normalized;

                uvs[vertIndex] = new Vector2((float)j / _segments, (float)i / _segments);

                if (i < _segments && j < _segments)
                {
                    triangles[triIndex + 0] = vertIndex;
                    triangles[triIndex + 1] = vertIndex + _segments + 1;
                    triangles[triIndex + 2] = vertIndex + 1;

                    triangles[triIndex + 3] = vertIndex + 1;
                    triangles[triIndex + 4] = vertIndex + _segments + 1;
                    triangles[triIndex + 5] = vertIndex + _segments + 2;

                    triIndex += 6;
                }

                vertIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uvs;
    }
}
