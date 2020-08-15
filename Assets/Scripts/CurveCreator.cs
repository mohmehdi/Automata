using System;
using DefaultNamespace;
using UnityEngine;

public class CurveCreator : MonoBehaviour
{
    [SerializeField] private GameObject curvePrefab;

    private GameObject _currentCreated;
    private void Start()
    {
        StateConnectionEvents.Instance.OnFirstStateSelected += OnCreateCurveObject;
        StateConnectionEvents.Instance.OnSecondStateSelected += OnSetCurveOptions;
        StateConnectionEvents.Instance.OnSecondStateSelectionCanceled += OnDestroyCanceledCurve;
    }

    private void OnCreateCurveObject()
    {
        _currentCreated = Instantiate(curvePrefab, Vector3.zero, Quaternion.identity);
        _currentCreated.GetComponent<Curve>().SetFrom();
    }
    private void OnSetCurveOptions()
    {
        _currentCreated.GetComponent<Curve>().SetTo();
    }
    private void OnDestroyCanceledCurve()
    {
        if(!_currentCreated)
        Destroy(_currentCreated);
    }
}