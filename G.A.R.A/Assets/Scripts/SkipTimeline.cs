using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkipTimeline : MonoBehaviour
{
    PlayableDirector director;

    private void Start()
    {
        if(TryGetComponent<PlayableDirector>(out PlayableDirector pd))
        {
            director = pd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            director.time = director.playableAsset.duration;
            director.Evaluate();
            director.Stop();
        }
    }
}
