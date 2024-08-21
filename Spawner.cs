using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Material _material;

    private float _minPositionX = -11f;
    private float _maxPositionX = 11f;
    private float _minPositionZ = -11f;
    private float _maxPositionZ = 5f;
    private float _positionY = 7;

    private ObjectPool <GameObject> _cubesPool;

    private void Awake()
    {
        _cubesPool = new ObjectPool<GameObject>(
         createFunc: () => Instantiate(_cube),
         actionOnGet: (obj) => ActionOnGet(obj),
         actionOnRelease: (obj) => obj.SetActive(false),
         actionOnDestroy: (obj) => Destroy(obj),
         collectionCheck: true,
         defaultCapacity: _defaultCapacity,
         maxSize: _maxCapacity);
    }

    void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void Release(GameObject obj)
    { 
        _cubesPool.Release(obj);
    }

    private void ActionOnGet(GameObject obj)
    {
        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);       
        
        obj.transform.rotation = Quaternion.Euler(Vector3.zero); 
        obj.transform.position = new Vector3(positionX, _positionY, positionZ); ;        
        
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        obj.GetComponent<MeshRenderer>().material = _material;        
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _cubesPool.Get();
    }    
}
