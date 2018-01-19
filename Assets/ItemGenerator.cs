using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;


    public Vector3 unitychan;
    public GameObject unitychanInfo;
    public bool getCoin;

    public List<GameObject> lobj = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        

        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                    lobj.Add(cone);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;

                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                        lobj.Add(coin);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                        lobj.Add(car);
                    }
                }
            }
        }

        for (int cnt = 0; cnt < lobj.Count; cnt++)
        {
            Debug.Log(lobj[cnt].transform.position.z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //座標の取得
        unitychan = GameObject.Find("unitychan").transform.position;

        Debug.Log(getCoin);

        //各座標との比べ
        for (int cnt = 0; cnt < lobj.Count; cnt++)
        {
            if(lobj[cnt] == null)
            {
                lobj.Remove(lobj[cnt]);
            }
            if ((unitychan.z - 10 > lobj[cnt].transform.position.z))
            {
                Destroy(lobj[cnt]);
                lobj.Remove(lobj[cnt]);
            }
        }

    }
}