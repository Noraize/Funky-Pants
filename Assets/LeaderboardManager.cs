using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{
    private string URL_STEM = "http://dreamlo.com/lb/";
    private string URL_PRIVATE_CODE = "mzqeTvEOzU2sujDSAJBj2whdcsf-welEmUjPvDtG0QtQ";
    private string URL_PUBLIC_CODE = "60ec0e028f40bc11f0d0aa1a";

    [SerializeField] string scoreFormat = "pipe";

    // Start is called before the first frame update
    void Start()
    {
        getAllHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator callDreamLo(string url){
        WWW webreq = new WWW(url);
        yield return webreq;

        if(string.IsNullOrEmpty(webreq.error)){
            Debug.Log(webreq.text);
        }
        else {
            Debug.Log(webreq.error);
        }
    }

    public void getAllHighScores(){
        StartCoroutine(callDreamLo(URL_STEM+URL_PUBLIC_CODE+"/"+scoreFormat));
    }

    
}
