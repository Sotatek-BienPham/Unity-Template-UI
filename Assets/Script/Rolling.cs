using UnityEngine;

public class Rolling : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);

    void Update()
    {
        // Quay vật thể mỗi khung hình
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
