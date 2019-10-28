using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public Text Health;

    public void SetHealth(int HP)
    {
        Health.text = HP.ToString();
    }
}
