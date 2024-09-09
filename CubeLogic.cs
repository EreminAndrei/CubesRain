using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material _finishMaterial;

    public Action <Cube> LifeEnded;
    
    private Rigidbody _rigidbody;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 position, Material material)
    {        
        transform.position = position;
        transform.rotation = Quaternion.identity;        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;       
        _meshRenderer.material = material;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Trigger>() != null)
        {
            if (_meshRenderer.material != _finishMaterial)
            {
                _meshRenderer.material = _finishMaterial;
                StartCoroutine(LifeCount());
            }
        }
    }

    private IEnumerator LifeCount()
    {         
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
        LifeEnded?.Invoke(this);        
    }    
}
