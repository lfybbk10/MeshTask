using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PrimitiveParameterEditor : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private InputField _inputField;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetValueToInputField);
        _inputField.onValueChanged.AddListener(SetValueToSlider);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetValueToInputField);
        _inputField.onValueChanged.RemoveListener(SetValueToSlider);
    }

    public void AddListener(UnityAction<float> action)
    {
        _slider.onValueChanged.AddListener(action);
        _inputField.onValueChanged.AddListener((text =>
        {
            try
            {
                float value = float.Parse(text);
                action.Invoke(value);
            }
            catch (Exception e)
            {
                print(e);
            }
        }));
    }

    private void SetValueToInputField(float value)
    {
        _inputField.SetTextWithoutNotify(value.ToString());
    }
    
    private void SetValueToSlider(string value)
    {
        try
        {
            float newValue = float.Parse(value);
            _slider.SetValueWithoutNotify(newValue);
            _inputField.text = Math.Clamp(newValue, _slider.minValue, _slider.maxValue).ToString();
        }
        catch (Exception e)
        {
            print(e);
        }
    }
}
