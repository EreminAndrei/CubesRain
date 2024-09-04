using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] public GameObject _prefab;
    [SerializeField] private Material[] _materials = new Material[2];

    private ObjectPool <CubeLogic> _pool;
    
    private Rigidbody _rigidbody;

    private MeshRenderer _meshRenderer;  
    
    public void Init(Vector3 position, ObjectPool<CubeLogic> pool)
    { 
        _pool = pool;
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        transform.position = position;
        transform.rotation = Quaternion.identity;        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;       
        _meshRenderer.material = _materials[0];
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Trigger>() != null)
        {
            if (_meshRenderer.material != _materials[1])
            {
                _meshRenderer.material = _materials[1];
                StartCoroutine(LifeCount(this, _pool));
            }
        }
    }

    private IEnumerator LifeCount(CubeLogic cube, ObjectPool <CubeLogic> pool)
    {         
        yield return new WaitForSeconds(Random.Range(2, 6));
        Release();        
    }

    private void Release()
    {
        _pool.Release(this);
    }
}
