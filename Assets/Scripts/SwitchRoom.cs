using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRoom : MonoBehaviour
{
    [SerializeField] Image fadeImg;

    [SerializeField] GameObject[] rooms;

    [SerializeField] float fadeSpeed = 1.3f;

    private int currentRoomIndex = 0;

    public void ChangeRoom(int index){
        if(index == currentRoomIndex) return;
        StartCoroutine(Fade(index));
    }

    IEnumerator Fade(int index){
        fadeImg.color = new Color(0, 0, 0, 0);
        while(fadeImg.color.a < 1){
            fadeImg.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        rooms[currentRoomIndex].SetActive(false);
        rooms[index].SetActive(true);
        currentRoomIndex = index;
        while(fadeImg.color.a > 0){
            fadeImg.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
