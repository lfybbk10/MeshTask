using UnityEngine;

public class Prism : Primitive
{
    [SerializeField] private PrimitiveParameterEditor _heightEditor, _sidesEditor, _radiusEditor;
    private float _height = 2f;
    private int _sides = 6;
    private float _radius = 1f;
    
    protected override void AddListenersToParameterEditors()
    {
        _heightEditor.AddListener((value =>
        {
            _height = value;
            GenerateMesh();
        }));
        _sidesEditor.AddListener((value =>
        {
            _sides = (int) value;
            GenerateMesh();
        }));
        _radiusEditor.AddListener((value =>
        {
            _radius = value;
            GenerateMesh();
        }));
    }

    protected override void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        _meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[_sides * 4];
        Vector3[] normals = new Vector3[vertices.Length];
        int[] triangles = new int[_sides * 12];

        float angleStep = 2 * Mathf.PI / _sides;
        
        for (int i = 0; i < _sides; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            
            vertices[i] = new Vector3(x, 0, z);
            normals[i] = new Vector3(x, 0, z).normalized;
            
            vertices[i + _sides] = new Vector3(x, _height, z);
            normals[i + _sides] = new Vector3(x, 0, z).normalized;
            
            vertices[i + _sides * 2] = new Vector3(x, 0, z);
            normals[i + _sides * 2] = Vector3.down;
            
            vertices[i + _sides * 3] = new Vector3(x, _height, z);
            normals[i + _sides * 3] = Vector3.up;
        }

        int triIndex = 0;
        for (int i = 0; i < _sides; i++)
        {
            int next = (i + 1) % _sides;
            
            triangles[triIndex++] = i;
            triangles[triIndex++] = i + _sides;
            triangles[triIndex++] = next;

            triangles[triIndex++] = next;
            triangles[triIndex++] = i + _sides;
            triangles[triIndex++] = next + _sides;
            
            triangles[triIndex++] = _sides * 2 + i;
            triangles[triIndex++] = _sides * 2 + next;
            triangles[triIndex++] = _sides * 2;
            
            triangles[triIndex++] = _sides * 3 + i;
            triangles[triIndex++] = _sides * 3;
            triangles[triIndex++] = _sides * 3 + next;
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
    }

}
