using System.Collections.Generic;
using UnityEngine;

public class SingleMainThreadDispatcher : MonoBehaviour
{
    public static SingleMainThreadDispatcher Instance;
    private readonly Queue<System.Action> _executionQueue = new Queue<System.Action>();

    private void Awake()
    {
        // Ensure that only one instance of this class exists.
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the object persists across scenes
        }
        else {
            Destroy(gameObject); // Destroy any additional instances
        }
    }

    public void Enqueue(System.Action action)
    {
        lock (_executionQueue) {
            _executionQueue.Enqueue(action);
        }
    }

    void Update()
    {
        System.Action actionToExecute = null;
        do {
            lock (_executionQueue) {
                if (_executionQueue.Count > 0) {
                    actionToExecute = _executionQueue.Dequeue();
                }
                else
                    actionToExecute = null;
            }
            actionToExecute?.Invoke();
        } while (actionToExecute != null);
    }
}
