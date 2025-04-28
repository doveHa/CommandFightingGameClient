/*
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserMainScene
{
    public class MatchingBTN : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI matchingBTN;
        private float time;
        private bool isMatching = false;

        public void matchingStart()
        {
            if (UserMainScene.CharacterManager.CharManager().GetSelectName() == null)
            {
                Debug.Log("Select Character...");
      Character      }
            else
            {
                if (isMatching)
                {
                    isMatching = false;
                    Client.EndMatching();
                    Peer.EndMatching();
                    matchingBTN.text = "Matching";
                    time = 0;
                }
                else
                {
                    isMatching = true;
                    Debug.Log("WaitMatching");
                    Client.WaitMatching();
                    Debug.Log("P2PMatchingStart");
                    Peer.P2PMatchingStart();
                }
            }
        }

        void Update()
        {
            if (isMatching)
            {
                time += Time.deltaTime;
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                matchingBTN.text = $"{min}:{sec:D2}";
            }

            if (Peer.isP2PConnect)
            {
                isMatching = false;
                CharacterManager.CharManager().LoadingGameScene();
                Peer.isP2PConnect = false;
                //SceneManager.LoadScene("Scenes/SampleScene");
                //Debug.Log("loadScene");
            }
        }
    }
}*/