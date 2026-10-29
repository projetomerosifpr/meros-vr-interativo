using UnityEngine;

public class ProgressionControler : MonoBehaviour
{
    private bool[] _interctedWithCrab = new bool[2];
    private bool _isInPolution = false;
    public bool IsInPolution
    {
        get=>_isInPolution;
        set=>_isInPolution = value;
    }
    private int _isInDangerZone = 0;
    public int IsInDangerZone
    {
        get=>_isInDangerZone;
        set=> _isInDangerZone = value;
    }
    
    public static ProgressionControler _instance;

    void Awake()
    {
        if (_instance == null) _instance = this;
        DontDestroyOnLoad(_instance);
    }

    void Start()
    {
        _interctedWithCrab[0] = false;
        _interctedWithCrab[1] = false;
    }

    public bool GetInteractedWithCrab(int index)
    {
        return _interctedWithCrab[index];
    }

    public void SetInteractedWithCrab(bool value, int num)
    {
        _interctedWithCrab[num] = value;
    }
    
    
}
