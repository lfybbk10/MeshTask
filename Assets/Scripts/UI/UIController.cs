using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] private Primitive _cuboid, _sphere, _prism, _capsule;
    [SerializeField] private GameObject _cuboidWindow, _sphereWindow, _prismWindow, _capsuleWindow;
    [SerializeField] private Dropdown _primitiveDropdown;
    [SerializeField] private Button _addPrimitive, _removePrimitive;

    public void OnDropdownValueChanged()
    {
        switch (_primitiveDropdown.value)
        {
            case 0:
                HandleEnableDisableButtons(_cuboid);
                DisableAllParameterWindows();
                _cuboidWindow.gameObject.SetActive(true);
                break;
            case 1:
                HandleEnableDisableButtons(_sphere);
                DisableAllParameterWindows();
                _sphereWindow.gameObject.SetActive(true);
                break;
            case 2:
                HandleEnableDisableButtons(_prism);
                DisableAllParameterWindows();
                _prismWindow.gameObject.SetActive(true);
                break;
            case 3:
                HandleEnableDisableButtons(_capsule);
                DisableAllParameterWindows();
                _capsuleWindow.gameObject.SetActive(true);
                break;
        }
    }

    public void AddRemovePrimitive(bool value)
    {
        switch (_primitiveDropdown.value)
        {
            case 0:
                _cuboid.gameObject.SetActive(value);
                HandleEnableDisableButtons(_cuboid);
                break; 
            case 1:
                _sphere.gameObject.SetActive(value);
                HandleEnableDisableButtons(_sphere);
                break;  
            case 2:
                _prism.gameObject.SetActive(value);
                HandleEnableDisableButtons(_prism);
                break;
            case 3:
                _capsule.gameObject.SetActive(value);
                HandleEnableDisableButtons(_capsule);
                break;
        }
    }

    private void HandleEnableDisableButtons(Primitive primitive)
    {
        _addPrimitive.gameObject.SetActive(!primitive.gameObject.activeSelf);
        _removePrimitive.gameObject.SetActive(primitive.gameObject.activeSelf);
    }

    private void DisableAllParameterWindows()
    {
        foreach (var primitiveParameterEditor in FindObjectsOfType<PrimitiveParameterEditor>())
        {
            primitiveParameterEditor.transform.parent.gameObject.SetActive(false);
        }
    }
    
    
}
