
using System.Collections.Generic;

public interface IPresenter
{
    void RenderInventory(List<Item> list);
    void RenderStash(List<Item> list);
}
