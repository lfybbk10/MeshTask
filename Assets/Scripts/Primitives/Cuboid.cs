using UnityEngine;


public class Cuboid : Primitive
{
    [SerializeField] private PrimitiveParameterEditor _widthEditor, _heightEditor, _lengthEditor;
    private float _width = 1f;
    private float _height = 1f;
    private float _length = 1f;

    protected override void AddListenersToParameterEditors()
    {
        _widthEditor.AddListener((value =>
        {
            _width = value;
            GenerateMesh();
        }));
        _heightEditor.AddListener((value =>
        {
            _height = value;
            GenerateMesh();
        }));
        _lengthEditor.AddListener((value =>
        {
            _length = value;
            GenerateMesh();
        }));
    }

    protected override void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        _meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-_width/2, -_height/2, _length/2),
            new Vector3(_width/2, -_height/2, _length/2),
            new Vector3(_width/2, _height/2, _length/2),
            new Vector3(-_width/2, _height/2, _length/2),

            new Vector3(-_width/2, -_height/2, -_length/2),
            new Vector3(_width/2, -_height/2, -_length/2),
            new Vector3(_width/2, _height/2, -_length/2),
            new Vector3(-_width/2, _height/2, -_length/2),

            new Vector3(-_width/2, -_height/2, -_length/2),
            new Vector3(-_width/2, _height/2, -_length/2),
            new Vector3(-_width/2, _height/2, _length/2),
            new Vector3(-_width/2, -_height/2, _length/2),

            new Vector3(_width/2, -_height/2, -_length/2),
            new Vector3(_width/2, _height/2, -_length/2),
            new Vector3(_width/2, _height/2, _length/2),
            new Vector3(_width/2, -_height/2, _length/2),

            new Vector3(-_width/2, _height/2, -_length/2),
            new Vector3(_width/2, _height/2, -_length/2),
            new Vector3(_width/2, _height/2, _length/2),
            new Vector3(-_width/2, _height/2, _length/2),

            new Vector3(-_width/2, -_height/2, -_length/2),
            new Vector3(_width/2, -_height/2, -_length/2),
            new Vector3(_width/2, -_height/2, _length/2),
            new Vector3(-_width/2, -_height/2, _length/2)
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,

            4, 6, 5,
            4, 7, 6,

            8, 10, 9,
            8, 11, 10,

            12, 13, 14,
            12, 14, 15,

            16, 18, 17,
            16, 19, 18,

            20, 21, 22,
            20, 22, 23
        };

        Vector3[] normals = new Vector3[]
        {
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,

            Vector3.back, Vector3.back, Vector3.back, Vector3.back,

            Vector3.left, Vector3.left, Vector3.left, Vector3.left,

            Vector3.right, Vector3.right, Vector3.right, Vector3.right,

            Vector3.up, Vector3.up, Vector3.up, Vector3.up,

            Vector3.down, Vector3.down, Vector3.down, Vector3.down
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
    }
}