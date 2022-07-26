using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text m_timeText;
    private int time;

    [SerializeField] Text m_heathText;
    [SerializeField] Text m_expText;

    //������
    public Image[] m_activeItemImage = new Image[4];
    public Image[] m_passiveItemImage = new Image[4];

    //������ư
    [SerializeField] GameObject m_levelUpPanel;
    [SerializeField] Button[] m_buttons = new Button[3];
    [SerializeField] Image[] m_buttonImages = new Image[3];
    [SerializeField] Text[] m_buttonTexts = new Text[3];

    private static UIManager _instance;

    //����
    [SerializeField] Weapon[] m_weapons = new Weapon[10];
    [SerializeField] private int m_totalActiveWeaponCount = 4;
    [SerializeField] private int m_totalPassiveWeaponCount = 6;
    private int m_totalWeaponCount;
    



    /// <�̱���>
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;

                if (_instance == null)
                {
                    Debug.Log("no singleton");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        ///////////////////////////////////////////////////

        m_totalWeaponCount = m_totalActiveWeaponCount + m_totalPassiveWeaponCount;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_timeText.text = "00:00";
    }


    public void SetHealth(int max, int current)
    {
        m_heathText.text = current + "/" + max;
    }

    public void SetExp(int max, int current)
    {
        m_expText.text = current + "/" + max;

        if(current >= max)
        {
            
        }

    }

    public void SayHi()
    {
        Debug.Log("hi");
    }


    public void DisableLevelUpPanel()
    {
        m_levelUpPanel.gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        m_levelUpPanel.gameObject.SetActive(true);
        RandomizeButtons();
        for(int i=0; i<3; i++)
        {
            m_buttons[i].onClick.RemoveAllListeners();
            m_buttons[i].onClick.AddListener(GameManager.Instance.Resume);
            int index = i;
            //����ĭ�� �̹����ֱ�
            m_buttons[i].onClick.AddListener(()=> SetItemImage(index));
            m_buttons[i].onClick.AddListener(DisableLevelUpPanel);
            //���⿡�� �˷��ִ��Լ��߰�
            m_buttons[i].onClick.AddListener(() => LevelUpWeapon(index));
            
        }
        
        
        m_buttons[0].onClick.AddListener(SayHi);
        
    }

    private void RandomizeButtons()
    {

        //�����ϰ� ���ڻ̰�
        //���⿡�� �̸��̶� �����̾ƿͼ�
        //
        //��Ƽ��
        //if(Random.Range(0,1) == 0)
        //������ �ϵ��ڵ��� ���� 
        //�̻ڰ� �������ұ�?
        int random1, random2, random3;
        if (true)
        {
            random1 = Random.Range(0, m_totalWeaponCount);
            m_buttonImages[0].sprite = m_weapons[random1].m_iconImage;
            m_buttons[0].GetComponentInChildren<Text>().text = m_weapons[random1].m_weaponName;

            random2 = Random.Range(0, m_totalWeaponCount);
            while(random1 == random2)
            {
                random2 = Random.Range(0, m_totalWeaponCount);
            }
            m_buttonImages[1].sprite = m_weapons[random2].m_iconImage;
            m_buttons[1].GetComponentInChildren<Text>().text = m_weapons[random2].m_weaponName;

            random3 = Random.Range(0, m_totalWeaponCount);
            while(random1 == random3 || random2 == random3)
            {
                random3 = Random.Range(0, m_totalWeaponCount);
            }
            m_buttonImages[2].sprite = m_weapons[random3].m_iconImage;
            m_buttons[2].GetComponentInChildren<Text>().text = m_weapons[random3].m_weaponName;

        }
        //�нú�
        else
        {

        }

    }

    //���� ����ĭ�� �̹��� �ֱ�
    private void SetItemImage(int index)
    {
        //���������� �ľ��ؼ�
        //��Ƽ���
        //�������鼭 ����ֳ� Ȯ��
        //��������� �׸���ü
        if(index > m_totalActiveWeaponCount)
        {
            for (int i = 0; i < m_totalWeaponCount; i++)
            {
                if (!m_activeItemImage[i].sprite)
                {
                    m_activeItemImage[i].sprite = m_buttonImages[index].sprite;
                    break;
                }
                else if (m_activeItemImage[i].sprite == m_buttonImages[index].sprite)
                {
                    break;
                }
            }
        }
        //�нú��
        else
        {
            for (int i = 0; i < m_totalWeaponCount; i++)
            {
                if (!m_passiveItemImage[i].sprite)
                {
                    m_passiveItemImage[i].sprite = m_buttonImages[index].sprite;
                    break;
                }
                else if (m_passiveItemImage[i].sprite == m_buttonImages[index].sprite)
                {
                    break;
                }
            }
        }



    }

    //��
    private void LevelUpWeapon(int index)
    {
        for(int i=0; i< m_totalWeaponCount; i++)
        {
            if(m_weapons[i].m_iconImage == m_buttonImages[index].sprite)
            {
                m_weapons[i].LevelUp();
                break;
            }
        }
    }
    

}
