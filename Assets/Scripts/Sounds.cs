using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public float loopFadeSeconds = 1f;

    private List<AudioSource> loop;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        this.loop = this.GetComponents<AudioSource>().Where(a => a.loop).ToList();
        Debug.Log($"Loading {this.loop.Count} AudioSources for loop");
        if (this.loop.Count > 0)
        {
            this.StartLoop(this.loop.First().clip.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Test the behavior by switching tracks every 5s
        this.seconds += Time.deltaTime;
        if (this.seconds >= 5)
        {
            this.seconds = 0;
            this.StartLoop(this.loop[(this.loop.FindIndex(0, a => a.isPlaying) + 1) % this.loop.Count].clip.name);
        }
        */
    }

    public void StartLoop(string name)
    {
        var current = this.loop.FirstOrDefault(a => a.isPlaying);
        if (current != null)
        {
            Debug.Log($"Stopping loop {current.clip.name}");
            this.StartCoroutine(Fade(current, this.loopFadeSeconds, false));
        }

        current = this.loop.FirstOrDefault(a => a.clip.name == name);
        if (current != null)
        {
            Debug.Log($"Playing loop {name}");
            this.StartCoroutine(Fade(current, this.loopFadeSeconds, true));
        } else {
            Debug.Log($"AudioSource {name} is not in loop to be played");
        }
    }

    public static IEnumerator Fade(AudioSource audioSource, float totalSeconds, bool play)
    {
        var elapsedSeconds = 0f;
        var startingVolume = audioSource.volume;
        var endingVolume = (play) ? 100f : 0f;

        while (elapsedSeconds < totalSeconds)
        {
            elapsedSeconds += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startingVolume, endingVolume, elapsedSeconds / totalSeconds);
            yield return null;
        }

        if (play) 
        {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }

        yield break;
    }
}
