using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Primitive : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker _colorPicker;
    protected MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        AddListenersToParameterEditors();
        GenerateMesh();
    }

    private void OnEnable()
    {
        _colorPicker.onColorChange.AddListener(UpdateColor);
    }

    private void OnDisable()
    {
        _colorPicker.onColorChange.RemoveListener(UpdateColor);
    }

    protected virtual void AddListenersToParameterEditors()
    {
        
    }

    protected virtual void GenerateMesh()
    {
        
    }

    private void UpdateColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
