using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator LoadGameFromMenu(string scene)
    {
        //Play Animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(0.8f);
        //Do the thing
        SceneManager.LoadScene(scene);
    }
}
