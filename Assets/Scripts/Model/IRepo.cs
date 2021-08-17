
using System.Collections.Generic;

public interface IRepo
{
    Bag GetInventData();
    void SaveInvent(Bag bag);
    void SaveStash(Bag bag);
    Bag GetStashData();
}

