using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public ModelPlayable playable;

    void Start()
    {
        if (!FindObjectOfType<ModelPlayable>())
        {
            ModelPlayable newPlayer = Instantiate(playable);
            newPlayer.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
