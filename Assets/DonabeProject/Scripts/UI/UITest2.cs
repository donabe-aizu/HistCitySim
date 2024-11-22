using UnityEngine;
using UnityEngine.UIElements;

public class UITest2 : MonoBehaviour
{
    [SerializeField] private GameObject moveObject;
    
    private Vector3Field Vector3Field;
    
    void Start()
    {
        Vector3Field = GetComponent<UIDocument>().rootVisualElement.Q<Vector3Field>("test-vector3");
        
        Vector3Field.RegisterValueChangedCallback(x => moveObject.transform.position = x.newValue);
    }

    void Update()
    {
        Vector3Field.value = moveObject.transform.position;
    }
}
