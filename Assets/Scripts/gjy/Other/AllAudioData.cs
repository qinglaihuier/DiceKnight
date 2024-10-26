using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllAudioData", menuName = "Data/AllAudioData")]
public class AllAudioData : ScriptableObject
{
    // Start is called before the first frame update
    public List<AudioData> datas = new List<AudioData>();

   
}
