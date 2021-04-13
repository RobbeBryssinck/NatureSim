using UnityEngine;

public class Walk : IWalkBehavior
{
    public float WalkTime = 5.0f;
    private float currentTime = 0.0f;
    private bool initWalk = true;

    Animator anim;
    Rigidbody rb;

    public bool Move()
    {
        if (initWalk)
        {
            // init walk
            initWalk = false;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= WalkTime)
        {
            currentTime = 0.0f;
            initWalk = true;
            return true;
        }

        // walk
        return false;
    }
}
