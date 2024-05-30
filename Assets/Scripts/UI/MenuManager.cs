using System;
using System.Diagnostics;
using MetaLabs.Core;
using MetaLabs.Interface;
using MetaLabs.Models;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace MetaLabs.UI
{
    public class MenuManager : MonoBehaviourPunCallbacks
    {
        public static MenuManager Instance;
        public Menu[] menuList;
        [Header("Login Attributes")]
        [SerializeField] TMP_InputField emailField;
        [SerializeField] TMP_InputField passwordField;
        [SerializeField] GameObject loginPage;
        [SerializeField] TMP_Text errorText;
        [SerializeField] ContinuousMoveProviderBase continuousMoveProviderBase;
        [SerializeField] GameObject loginExitBtn;
        [SerializeField] TMP_Text profileUserName;

        [Header("Objects to Visible after Login")] 
        [SerializeField] private GameObject[] _objects;


        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //Commented for Testing //continuousMoveProviderBase.enabled = false;
            if (SceneManager.GetActiveScene().name != "EnteryRoom")
            {
                OpenMenu("leave");
                foreach (GameObject obj in _objects)
                {
                    if (obj.name == "BottomStrip") continue;
                    obj.SetActive(false);
                }
                return;
            }
            if (PlayerPrefs.GetString("login") == "false")
            {
                foreach (GameObject obj in _objects)
                {
                    obj.SetActive(false);
                }
                loginPage.SetActive(true);
                loginExitBtn.SetActive(true);
                OpenMenu(""); // this will close all the menus
                
            }
            if (PlayerPrefs.GetString("login") == "true")
            {
                foreach (GameObject obj in _objects)
                {
                    obj.SetActive(true);
                };
            }
            if (PlayerPrefs.GetString("login") == "true" && SceneManager.GetActiveScene().name == "EnteryRoom")
            {
                loginPage.SetActive(false);
                OpenMenu("experience");
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (profileUserName.text != null) profileUserName.text = PlayerPrefs.GetString("member_name");

        }

        public void OpenMenu(string menu)
        {
            //FindObjectOfType<AudioManager>().Play("click");
            if(string.IsNullOrEmpty(menu)) return;
            for (int i = 0; i < menuList.Length; i++)
            {
                if (menu == menuList[i].name)
                {
                    menuList[i].gameObject.SetActive(true);
                }
                else
                {
                    menuList[i].gameObject.SetActive(false);
                }
            }
        }

        public void LogIn()
        {
            if (emailField.text.ToLower() == "user@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "kamini@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "achyut@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "abhinav@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "divya@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "shehran@demo.com" && passwordField.text == "user123" ||
                emailField.text.ToLower() == "gopi@demo.com" && passwordField.text == "user123")
            {
                if (emailField.text.ToLower() == "user@demo.com") // Hemtanu
                {
                    PlayerPrefs.SetString("email", "user@demo.com");
                    PlayerPrefs.SetString("member_name", "Guest User");
                }
                if (emailField.text.ToLower() == "kamini@demo.com") // Kamini
                {
                    PlayerPrefs.SetString("email", "kamini@demo.com");
                    PlayerPrefs.SetString("member_name", "Kamini Malik");
                }
                if (emailField.text.ToLower() == "achyut@demo.com") // Achyut
                {
                    PlayerPrefs.SetString("email", "achyut@demo.com");
                    PlayerPrefs.SetString("member_name", "Achyut Chandra");
                }
                if (emailField.text.ToLower() == "abhinav@demo.com") // Abhinav
                {
                    PlayerPrefs.SetString("email", "abhinav@demo.com");
                    PlayerPrefs.SetString("member_name", "Abhinav Khare");
                }
                if (emailField.text.ToLower() == "divya@demo.com") // Divya
                {
                    PlayerPrefs.SetString("email", "divya@demo.com");
                    PlayerPrefs.SetString("member_name", "Divya Sardana");
                }
                if (emailField.text.ToLower() == "neha@demo.com") // Neha
                {
                    PlayerPrefs.SetString("email", "neha@demo.com");
                    PlayerPrefs.SetString("member_name", "Neha Sharma");
                }
                if (emailField.text.ToLower() == "gopi@demo.com") // Gopi
                {
                    PlayerPrefs.SetString("email", "gopi@demo.com");
                    PlayerPrefs.SetString("member_name", "Gopi K");
                }
                if (emailField.text.ToLower() == "shehran@hcl.com") // Shehran
                {
                    PlayerPrefs.SetString("email", "shehran@hcl.com");
                    PlayerPrefs.SetString("member_name", "Shehran Siddiqui");
                }
                PlayerPrefs.SetString("nickname", PlayerPrefs.GetString("member_name"));
                loginPage.SetActive(false);
                loginExitBtn.SetActive(false);
                //if (profileUserName.text != null) profileUserName.text = PlayerPrefs.GetString("member_name");
                OpenMenu("experience");
                PlayerPrefs.SetString("login", "true");
                //continuousMoveProviderBase.enabled = true;
                foreach (GameObject obj in _objects)
                {
                    obj.SetActive(true);
                }
                
            }
            else
            {
                errorText.text = "Incorrect email or password";
                return;
            }
        }

        public void ProfileDropDown(int i)
        {
            if (i == 0) // User's Name
            {
                // Do nothing
            }

            if (i == 1) // Profile
            {
                OpenMenu("profile");
            }

            if (i == 2) // Log Out
            {
                Application.Quit();
                PlayerPrefs.SetString("login", "false");
                //FindObjectOfType<AudioManager>().Play("click");
            }
        }

        public void SignOut()
        {

            PlayerPrefs.SetString("login", "false");
            if (ProductConfiguration.Instance.isDeviceVR)
            {
                Application.Quit();
                return;
            }
            if (!Application.isEditor)
            {
                Process.GetCurrentProcess().Kill();
            }
            Application.Quit();
            

        }

        public void Exit()
        {

            if (ProductConfiguration.Instance.isDeviceVR)
            {
                Application.Quit();
                return;
            }
            if (!Application.isEditor)
            {
                Process.GetCurrentProcess().Kill();
            }
            Application.Quit();


        }
    }
}