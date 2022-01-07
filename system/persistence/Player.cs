using UnityEngine;

public class Player : MonoBehaviour, IPersistence
{
    private void Awake()
    {
        PersistenceManager.Instance.Register(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) transform.localPosition += Vector3.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) transform.localPosition += Vector3.left;
    }

    private void OnDestroy()
    {
        PersistenceManager.Instance.Unregister(this);
    }

    public void OnSave(PersistenceData persistenceData)
    {
        Vector3 localPosition = transform.localPosition;
        float[] lastPosition = { localPosition.x, localPosition.y, localPosition.z };
        Vector3 forward = transform.forward;
        float[] lastDirection = { forward.x, forward.y, forward.z };
        persistenceData.PlayerData = new PlayerData(lastPosition, lastDirection);
    }

    public void OnLoad(PersistenceData persistenceData)
    {
        PlayerData playerData = persistenceData.PlayerData;
        transform.localPosition = new Vector3(playerData.LastPosition[0], playerData.LastPosition[1], playerData.LastPosition[2]);
        transform.forward = new Vector3(playerData.LastDirection[0], playerData.LastDirection[1], playerData.LastDirection[2]);
    }
}