using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 
public class Counters : MonoBehaviour
{
    private int rabbitCount;
    private int wolfCount;
    
    [SerializeField] InputField field;
 
    public void RabbitCounter()
    {
        if (field.text != null)
        {
            this.rabbitCount = int.Parse(field.text);
            // Debug.Log(RabbitCount);


        }
    }

    // public int RCgetter()
    // {
    //     return this.RabbitCount;
    // }
    
    public void WolfCounter()
    {
        if (field.text != null)
        {
            this.wolfCount = int.Parse(field.text);
            // Debug.Log(WolfCount);
        }
    }
    // public int WCgetter()
    // {
    //     return this.WolfCount;
    // }
    
}
