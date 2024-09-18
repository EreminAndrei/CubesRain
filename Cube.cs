using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private  Color [] _finisColors = new Color[3];
    [SerializeField] private Color _startColor;
    
    private Rigidbody _rigidbody;

    private MeshRenderer _meshRenderer;

    private bool _isColorChanged;

    public event Action <Cube> LifeEnded;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            if (_isColorChanged == false)
            {
                Color finishColor = _finisColors[UnityEngine.Random.Range(0, _finisColors.Length)];
                _meshRenderer.material.color = finishColor;
                _isColorChanged = true;
                StartCoroutine(LifeTimeCounting());
            }
        }
    }

    public void Init(Vector3 position)
    {
        _meshRenderer.material.color = _startColor;
        _isColorChanged = false;
        transform.position = position;
        transform.rotation = Quaternion.identity;        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;           
        gameObject.SetActive(true);
    }

    private IEnumerator LifeTimeCounting()
    {         
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
        LifeEnded?.Invoke(this);        
    }    
}
