using UnityEngine;

public class DashTrailObject : MonoBehaviour
{
    public SpriteRenderer mRenderer;
    public Color mStartColor, mEndColor;

    private float mDisplayTime;
    private float mTimeDisplayed;
    private DashTrail mSpawner;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        mTimeDisplayed += Time.deltaTime;

        mRenderer.color = Color.Lerp(mStartColor, mEndColor, mTimeDisplayed / mDisplayTime);

        if (mTimeDisplayed >= mDisplayTime)
        {
            mSpawner.RemoveTrailObject(gameObject);
            Destroy(gameObject);
        }
    }

    public void Initiate(float displayTime, Sprite sprite, DashTrail trail)
    {
        mDisplayTime = displayTime;
        mRenderer.sprite = sprite;
        mTimeDisplayed = 0;
        mSpawner = trail;
    }
}