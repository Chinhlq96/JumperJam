using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    public static DashTrail Instance;
    public SpriteRenderer mLeadingSprite;

    public int mTrailSegments;
    public float mTrailTime;
    public GameObject mTrailObject;

    private float mSpawnInterval;
    private float mSpawnTimer;
    public bool mbEnabled;

    private List<GameObject> mTrailObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    private void Start()
    {
        mSpawnInterval = mTrailTime / mTrailSegments;
        mTrailObjects = new List<GameObject>();
        mbEnabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (mbEnabled)
        {
            mSpawnTimer += Time.deltaTime;

            if (mSpawnTimer >= mSpawnInterval)
            {
                GameObject trail = GameObject.Instantiate(mTrailObject);
                //  GameObject trail = ContentMgr.Instance.GetItem("DashTrailObject");
                DashTrailObject trailObject = trail.GetComponent<DashTrailObject>();

                trailObject.Initiate(mTrailTime, mLeadingSprite.sprite, this);
                trail.transform.position = transform.position;
                trail.transform.localScale = mLeadingSprite.gameObject.transform.localScale;
				trail.transform.eulerAngles = transform.eulerAngles;
                mTrailObjects.Add(trail);

                mSpawnTimer = 0;
            }
        }
    }

    public void RemoveTrailObject(GameObject obj)
    {
        mTrailObjects.Remove(obj);
    }

    public void SetEnabled(bool enabled)
    {
        mbEnabled = enabled;

        if (enabled)
        {
            mSpawnTimer = mSpawnInterval;
        }
    }
}