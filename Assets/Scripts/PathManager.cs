using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public Transform[] waypoints;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
}
