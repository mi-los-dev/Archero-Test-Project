using UnityEngine;

public interface IGun
{
    public bool TryShot(GameObject shooter, Vector3 start, Vector3 forward);
}