using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Viev : MonoBehaviour,IPresenter
{
    [SerializeField] private GameObject[] _inventorySlots;
    [SerializeField] private GameObject[] _stashSlots;
    [SerializeField] Button _moveItem;
    [SerializeField] Button _exitButton;
    private Presenter _presenter;
    private int _defaultItemIndex = 0;
    private bool _stashOrInvent;

    void Start()
    {
        _presenter = new Presenter(this);
      _presenter.LoadData();
        InicializeButtons();
        ChangeButtonStatus(false);
        _moveItem.onClick.AddListener(IndexChanger);
        _moveItem.onClick.AddListener(() => ChangeButtonStatus(false));
        _moveItem.onClick.AddListener(() => ButtonLight());
        _exitButton.onClick.AddListener(SaveAndExit);
    }
    public void RenderInventory(List<Item> list)
    {
        foreach (var item in _inventorySlots)
        {
            item.SetActive(false);
            
        }
        for (int i = 0; i < list.Count; i++)
        {
            var spr = Resources.Load<Sprite>("Sprites/" + list[i].ImgName);
            _inventorySlots[i].SetActive(true);
            _inventorySlots[i].GetComponent<Image>().sprite = spr;
        }
    }
    public void RenderStash(List<Item> list)
    {
        foreach (var item in _stashSlots)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < list.Count; i++)
        {
            var spr = Resources.Load<Sprite>("Sprites/" + list[i].ImgName);
            _stashSlots[i].SetActive(true);
            _stashSlots[i].GetComponent<Image>().sprite = spr;
        }
    }
    private void ComponentIndex(string name)
    {
        _defaultItemIndex = int.Parse(name);
    }
    private void StashOrInvent(bool changer)
    {
        _stashOrInvent = changer;
    }
    private void ChangeButtonStatus(bool status)
    {
        _moveItem.interactable = status;
    }
    private void InicializeButtons()
    {
        foreach (var item in _inventorySlots)
        {
          
            item.GetComponent<Button>().onClick.AddListener(() => ComponentIndex(item.name));
            item.GetComponent<Button>().onClick.AddListener(() => StashOrInvent(true));
            item.GetComponent<Button>().onClick.AddListener(() => ChangeButtonStatus(true));
            item.GetComponent<Button>().onClick.AddListener(() => ButtonLight(item));
        }
        foreach (var item in _stashSlots)
        {
            item.GetComponent<Button>().onClick.AddListener(() => ComponentIndex(item.name));
            item.GetComponent<Button>().onClick.AddListener(() => StashOrInvent(false));
            item.GetComponent<Button>().onClick.AddListener(() => ChangeButtonStatus(true));
            item.GetComponent<Button>().onClick.AddListener(() => ButtonLight(item));
        }
    }
    private void IndexChanger()
    {if(_stashOrInvent)
            _presenter.ChangeInventory(_defaultItemIndex);
  
    if(!_stashOrInvent)
            _presenter.ChangeStash(_defaultItemIndex);
    }

    private void ButtonLight(GameObject buttonObj)
    {
        if (buttonObj.transform.GetChild(0).gameObject.activeSelf == true)
        {
            buttonObj.transform.GetChild(0).gameObject.SetActive(false);
            ChangeButtonStatus(false);
            foreach (var item in _inventorySlots)
            { if(item.GetHashCode() != buttonObj.GetHashCode())item.transform.GetChild(0).gameObject.SetActive(false); }
            foreach (var item in _stashSlots)
            { if (item.GetHashCode() != buttonObj.GetHashCode()) item.transform.GetChild(0).gameObject.SetActive(false); }
        }
        else if (buttonObj.transform.GetChild(0).gameObject.activeSelf == false)
        {
            buttonObj.transform.GetChild(0).gameObject.SetActive(true);
            foreach (var item in _inventorySlots)
            { if (item.GetHashCode() != buttonObj.GetHashCode()) item.transform.GetChild(0).gameObject.SetActive(false); }
            foreach (var item in _stashSlots)
            { if (item.GetHashCode() != buttonObj.GetHashCode()) item.transform.GetChild(0).gameObject.SetActive(false); }
        }
    }
    private void ButtonLight()
    {
        foreach (var item in _inventorySlots)
        { item.transform.GetChild(0).gameObject.SetActive(false); }
        foreach (var item in _stashSlots)
        { item.transform.GetChild(0).gameObject.SetActive(false); }
    }
    private void SaveAndExit()
    {
        print("Save");
        Application.Quit();
    }
}



    




