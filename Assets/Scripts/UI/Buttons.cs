using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    AudioData audioData1, audioData2;

    public GameObject panel;

    private void Start() 
    {
        audioData1 = AudioManager.Instance.getAudioData(AudioName.打开菜单商店);
        audioData2 = AudioManager.Instance.getAudioData(AudioName.关闭菜单商店);
    }

    public void StartGame()
    {
        AudioManager.Instance.playVFX(audioData1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        AudioManager.Instance.playVFX(audioData2);
        Application.Quit();
    }

    public void OpenTips()
    {
        AudioManager.Instance.playVFX(audioData1);
        panel.SetActive(true);
    }

    public void CloseTips()
    {
        AudioManager.Instance.playVFX(audioData2);
        panel.SetActive(false);
    }
}
