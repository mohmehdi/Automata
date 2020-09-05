using System;
using UnityEngine;

public class CurveCreator : MonoBehaviour
{
    [SerializeField] private GameObject curvePrefab=null;

    private GameObject _currentCreated;
    private void Start()
    {
        ConnectionEvents.Instance.OnFirstStateSelected += OnCreateCurveObject;
        ConnectionEvents.Instance.OnSecondStateSelected += OnSetCurveOptions;
        ConnectionEvents.Instance.OnSecondStateSelectionCanceled += OnDestroyCanceledConnection;
    }

    private void OnCreateCurveObject()
    {
        _currentCreated = Instantiate(curvePrefab, Vector3.forward, Quaternion.identity);
        _currentCreated.GetComponent<Curve>().SetFrom();
    }
    private void OnSetCurveOptions()
    {
        _currentCreated.GetComponent<Curve>().SetTo();
    }
    private void OnDestroyCanceledConnection()
    {
        if(_currentCreated)
        Destroy(_currentCreated);
    }
}