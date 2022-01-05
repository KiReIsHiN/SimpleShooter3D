using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] Text _health;

    DamageHealth _dh;


    // Start is called before the first frame update
    void Start()
    {
        _dh = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowBulletsUI();
    }

    void ShowBulletsUI()
    {
        //���������� �� ������ ������� ���� � ������ � � ������
        _health.text = _dh.healthCount.ToString();
    }
}
