using UnityEngine;

public class joyStick : MonoBehaviour
{
   public RectTransform joyStickObj;
   public RectTransform Knob;

   private void Awake()
   {
      joyStickObj = GetComponent<RectTransform>();
   }
}
