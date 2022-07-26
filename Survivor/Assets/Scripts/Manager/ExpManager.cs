using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    List<ExpDiamond> m_expDiamondActiveList = new List<ExpDiamond>();
    List<ExpDiamond> m_expDiamondInActiveList = new List<ExpDiamond>();

    [SerializeField] private int m_maxExpPoint;
    [SerializeField] private int m_ExpPoint;
    [SerializeField] private int m_increaseMaxExp;

    public delegate void LevelUp();
    public static event LevelUp OnLevelUp;

    //싱글톤
    private static ExpManager _instance;


    public static ExpManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(ExpManager)) as ExpManager;

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

        InitializeExpDiamond();

        DontDestroyOnLoad(gameObject);
    }




    void InitializeExpDiamond()
    {
        m_maxExpPoint = 100;
        m_ExpPoint = 0;
        m_increaseMaxExp = 50;

        for (int i = 0; i < 30; i++)
        {
            ExpDiamond expDiamond = Instantiate(Resources.Load<ExpDiamond>("ExpDiamond"));
            expDiamond.gameObject.SetActive(false);
            m_expDiamondInActiveList.Add(expDiamond);
        }
    }

    public void DropExpDiamond(int expPoint, Vector3 pos)
    {
        m_expDiamondInActiveList[0].Initialize(expPoint, pos);  //세팅하고
        m_expDiamondInActiveList[0].gameObject.SetActive(true); //액티브 키고
        m_expDiamondActiveList.Add(m_expDiamondInActiveList[0]);    //액티브리스트에 추가하고
        m_expDiamondInActiveList.Remove(m_expDiamondInActiveList[0]);   //인액티브리스트에서 빼고
    }

    public void Reload(ExpDiamond expDiamond)
    {
        m_ExpPoint += expDiamond.m_expPoint;
        //경험치 꽉차면
        if (m_ExpPoint >= m_maxExpPoint)
        {
            m_ExpPoint = m_ExpPoint - m_maxExpPoint;
            m_maxExpPoint += m_increaseMaxExp;

            //랩업
            UIManager.Instance.LevelUp();
            GameManager.Instance.LevelUp();
            
        }
        //UI 보내고
        UIManager.Instance.SetExp(m_maxExpPoint, m_ExpPoint);
        expDiamond.gameObject.SetActive(false);

        //액티브리스트에서 빼야지
        m_expDiamondActiveList.Remove(expDiamond);
        //인액티브에넣고
        m_expDiamondInActiveList.Add(expDiamond);
    }

    public void OnMagnetActivate()
    {
        for(int i=0; i<m_expDiamondActiveList.Count; i++)
        {
            m_expDiamondActiveList[i].PickUp();
        }
    }

}
