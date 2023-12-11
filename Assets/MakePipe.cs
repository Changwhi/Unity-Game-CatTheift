using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePipe : MonoBehaviour
{
    public GameObject pipe;
    float timer = 0;
    public GameObject pipes;
    public float timeDiff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer> timeDiff)
        {
            GameObject instantiatedObject = Instantiate(pipe, pipes.transform, false); 
            instantiatedObject.transform.position = new Vector3(5, Random.Range(-5.0f, 4.5f), 0);
            timer = 0;
            Destroy(instantiatedObject, 8.0f);
        }
       
    }
}
