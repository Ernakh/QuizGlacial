/// ---------------------------------------------
/// Dyp The Penguin Character | Dypsloom
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.DypThePenguin.Scripts.Game
{
    using Dypsloom.DypThePenguin.Scripts.Damage;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// The nice penguin that the character should save.
    /// </summary>
    public class NicePenguin : MonoBehaviour
    {
        [Tooltip("The animator.")]
        [SerializeField] protected Animator m_Animator;
        [Tooltip("The damageables.")]
        [SerializeField] protected Damageable[] m_Damageables;
        [Tooltip("The text box.")]
        [SerializeField] protected GameObject m_TextBox;
        [Tooltip("The dialog text.")]
        [SerializeField] protected TextMeshProUGUI m_DialogText;

        public AudioClip audioClip;
        public AudioClip audioEnd;

        public GameObject fimJogo;

        protected int m_BrokenChainCount;
        private static readonly int s_Free = Animator.StringToHash("Free");

        /// <summary>
        /// Initialize components.
        /// </summary>
        private void Awake()
        {
            for (int i = 0; i < m_Damageables.Length; i++) { m_Damageables[i].OnDie += BrokeChain; }
            m_Animator.SetBool(s_Free,false);

            UpdateDialogText();
        }

        /// <summary>
        /// Broke a chain.
        /// </summary>
        private void BrokeChain()
        {

            m_BrokenChainCount++;

            AudioSource audio = this.GetComponent<AudioSource>();
            audio.PlayOneShot(audioClip);

            if (m_BrokenChainCount == m_Damageables.Length) {
                UpdateDialogText();
                m_Animator.SetBool(s_Free,true);
                m_TextBox.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Update the dialog text.
        /// </summary>
        public void UpdateDialogText()
        {
            if (m_BrokenChainCount == 0) {

                if (GameManager.Instance.EnemyKillCount == 0) {
                    m_DialogText.text = "Os pinguins brincalhões me acorrentaram aqui. Por favor, vença-os e me liberte das correntes.";
                    return;
                } 
            
                if (GameManager.Instance.EnemyKillCount == 1) {
                    m_DialogText.text = "Você venceu este pinguin, vença os outros!";
                    return;
                }
            
                if (GameManager.Instance.EnemyKillCount == 2) {
                    m_DialogText.text = "Você esta perto de vencer todos os pinguins!";
                    return;
                }
            
                if (GameManager.Instance.EnemyKillCount == 5) {
                    m_DialogText.text = "Você venceu todos os pinguins! Por favor, use a picareta para destruir as correntes!";
                    return;
                }
            
            
                return;
            }
        
            if (m_BrokenChainCount == 1) {
                m_DialogText.text = "Quase livre, quebre a outra corrente!";
                return;
            }
        
            if (m_BrokenChainCount == 2) {
                m_DialogText.text = "Estou Livre! Obrigado! \n\n\n";
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.PlayOneShot(audioEnd);
                fimJogo.SetActive(true);
                return;
            }
        
            m_DialogText.text = "...";
        }
    }
}
