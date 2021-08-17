using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Presenter
{
    private IPresenter _presenter;
    private IRepo _repo;
    private Bag _inventBag;
    private Bag _stashBag;
    private List<Item> _inventoryItemList;
    private List<Item> _stashItemList;
    public Presenter(IPresenter presenter)
    {
        _presenter = presenter;
    }
    public void LoadData()
    {
        _repo = new Repo();
        _inventBag = _repo.GetInventData();
        _stashBag = _repo.GetStashData();
        _inventoryItemList = _inventBag.Items;
        _stashItemList = _stashBag.Items;
        _presenter.RenderInventory(_inventBag.Items);
        _presenter.RenderStash(_stashBag.Items);
    }

    public void ChangeInventory(int index)
    {
        _stashItemList.Add(_inventoryItemList[index]);
        _inventoryItemList.RemoveAt(index);
        _repo.SaveInvent(_inventBag);
        _repo.SaveStash(_stashBag);
        Refresh();
    }
    public void ChangeStash(int index)
    {
        _inventoryItemList.Add(_stashItemList[index]);
        _stashItemList.RemoveAt(index);
        _repo.SaveInvent(_inventBag);
        _repo.SaveStash(_stashBag);
        Refresh();
    }
    private void Refresh()
    {
        _presenter.RenderStash(_stashItemList);
        _presenter.RenderInventory(_inventoryItemList);
    }
}
