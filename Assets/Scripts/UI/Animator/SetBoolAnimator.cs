using UnityEngine;

public class SetBoolAnimator : MonoBehaviour
{

    [Header("Animator Set Bool Settings:")]
    [SerializeField] private Animator anim;
    private string bool_string;
    
    public void SetBoolString(string boolString)
    {
        bool_string = boolString;
    }

    public void SetBool(bool value)
    {
        anim.SetBool(bool_string, value);
    }
}
