using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace xamformsdemo.CustomControls
{

  /// <summary>
  /// Just keeping this guy around for a bit because it shows how to
  /// get the (View)Cell associated with a given BindingContext (ViewModel)
  /// </summary>
  public class ExtendedListView : Xamarin.Forms.ListView
  {

    public ExtendedListView() : base(ListViewCachingStrategy.RecycleElement)
    {
    }

    /// <summary>
    /// Cast to a ViewCell/TextCell etc. in caller if that's what you're after.
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public Cell GetTemplatedItem(object itemData)
    {
      return TemplatedItems.First(cell => cell.BindingContext == itemData);
    }

  }
}