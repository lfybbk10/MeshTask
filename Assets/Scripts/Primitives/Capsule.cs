using UnityEngine;
using UnityEngine.UI;

public class Capsule : Primitive
{
    [SerializeField] private PrimitiveParameterEditor _radiusEditor, _heightEditor, _segmentsEditor;
    private float _radius = 0.7f;
    private float _height = 2f;
    private int _segments = 32;

    protected override void AddListenersToParameterEditors()
    {
        _radiusEditor.AddListener((value =>
        {
            _radius = value;
            GenerateMesh();
        }));
        _heightEditor.AddListener((value =>
        {
            _height = value;
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

        int vertCount = (_segments + 1) * (_segments + 1) * 2 + (_segments + 1) * (_segments + 1);
        Vector3[] vertices = new Vector3[vertCount];
        Vector3[] normals = new Vector3[vertCount];
        Vector2[] uv = new Vector2[vertCount];
        int[] triangles = new int[_segments * _segments * 12 + _segments * _segments * 6];

        int vertIndex = 0;
        int triIndex = 0;

        for (int y = 0; y <= _segments; y++)
        {
            for (int x = 0; x <= _segments; x++)
            {
                float xSegment = (float) x / _segments;
                float ySegment = (float) y / _segments;
                float xPos = Mathf.Cos(xSegment * Mathf.PI * 2.0f) * Mathf.Sin(ySegment * Mathf.PI / 2.0f);
                float yPos = Mathf.Cos(ySegment * Mathf.PI / 2.0f);
                float zPos = Mathf.Sin(xSegment * Mathf.PI * 2.0f) * Mathf.Sin(ySegment * Mathf.PI / 2.0f);

                vertices[vertIndex] = new Vector3(xPos, yPos, zPos) * _radius + Vector3.up * _radius * _height / 2;
                normals[vertIndex] = new Vector3(xPos, yPos, zPos);
                uv[vertIndex] = new Vector2(xSegment, ySegment);
                vertIndex++;
            }
        }

        for (int y = 0; y <= _segments; y++)
        {
            for (int x = 0; x <= _segments; x++)
            {
                float xSegment = (float) x / _segments;
                float ySegment = (float) y / _segments;
                float xPos = Mathf.Cos(xSegment * Mathf.PI * 2.0f) * Mathf.Sin(ySegment * Mathf.PI / 2.0f);
                float yPos = Mathf.Cos(ySegment * Mathf.PI / 2.0f);
                float zPos = Mathf.Sin(xSegment * Mathf.PI * 2.0f) * Mathf.Sin(ySegment * Mathf.PI / 2.0f);

                vertices[vertIndex] = new Vector3(xPos, -yPos, zPos) * _radius - Vector3.up * _radius * _height / 2;
                normals[vertIndex] = new Vector3(xPos, -yPos, zPos);
                uv[vertIndex] = new Vector2(xSegment, ySegment);
                vertIndex++;
            }
        }

        for (int y = 0; y <= _segments; y++)
        {
            for (int x = 0; x <= _segments; x++)
            {
                float xSegment = (float) x / _segments;
                float ySegment = (float) y / _segments;

                float xPos = Mathf.Cos(xSegment * Mathf.PI * 2.0f);
                float zPos = Mathf.Sin(xSegment * Mathf.PI * 2.0f);

                vertices[vertIndex] = new Vector3(xPos, ySegment * _height - _height / 2, zPos) * _radius;
                normals[vertIndex] = new Vector3(xPos, 0, zPos);
                uv[vertIndex] = new Vector2(xSegment, ySegment);
                vertIndex++;
            }
        }

        for (int y = 0; y < _segments; y++)
        {
            for (int x = 0; x < _segments; x++)
            {
                int i0 = y * (_segments + 1) + x;
                int i1 = i0 + 1;
                int i2 = i0 + (_segments + 1);
                int i3 = i2 + 1;

                triangles[triIndex++] = i0;
                triangles[triIndex++] = i1;
                triangles[triIndex++] = i2;

                triangles[triIndex++] = i1;
                triangles[triIndex++] = i3;
                triangles[triIndex++] = i2;
            }
        }

        int offset = (_segments + 1) * (_segments + 1);
        for (int y = 0; y < _segments; y++)
        {
            for (int x = 0; x < _segments; x++)
            {
                int i0 = offset + y * (_segments + 1) + x;
                int i1 = i0 + 1;
                int i2 = i0 + (_segments + 1);
                int i3 = i2 + 1;

                triangles[triIndex++] = i0;
                triangles[triIndex++] = i2;
                triangles[triIndex++] = i1;

                triangles[triIndex++] = i1;
                triangles[triIndex++] = i2;
                triangles[triIndex++] = i3;
            }
        }

        offset = 2 * (_segments + 1) * (_segments + 1);
        for (int y = 0; y < _segments; y++)
        {
            for (int x = 0; x < _segments; x++)
            {
                int i0 = offset + y * (_segments + 1) + x;
                int i1 = i0 + 1;
                int i2 = i0 + (_segments + 1);
                int i3 = i2 + 1;

                triangles[triIndex++] = i0;
                triangles[triIndex++] = i2;
                triangles[triIndex++] = i1;

                triangles[triIndex++] = i1;
                triangles[triIndex++] = i2;
                triangles[triIndex++] = i3;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }
}