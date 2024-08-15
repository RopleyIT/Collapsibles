using Microsoft.AspNetCore.Components;

namespace Tiling
{
    public partial class TileSet
    {
        // Note that the buttons used to reshow these tiles
        // once they have been hidden will appear in a
        // SectionOutlet with name TopRow at a location
        // you choose in the parent page or one of its
        // parent layouts. To make this work you have to:
        //   1. Add an @using Microsoft.AspNetCore.Components.Sections
        //      to the top of the razor file that has the
        //      SectionOutlet element in it, or to the
        //      _Imports.razor file for that project.
        //   2. Add a <SectionOutlet SectionName="TopRow"/>
        //      element in the parent page where the
        //      buttons are intended to be.
        //   3. Make sure you setup interactive server
        //      rendering in the top level app.razor file
        //      for the Routes element and for HeadOutlet
        //      using @rendermode="InteractiveServer"

        // The set of tiles being displayed inside this 
        // CollapsibleTiles component. Used to keep track
        // of what buttons to show for collapsed tiles.

        public List<Tile> Children { get; } = new();

        public void AddTile(Tile tile)
        {
            Children.Add(tile);
            tile.VisibleChanged = new EventCallbackFactory()
                .Create<Tile>(this, TileVisibilityChanged);
            StateHasChanged();
        }

        private void ShowTile(Tile tile)
            => tile.Visible = true;

        private void TileVisibilityChanged(Tile tile)
            => StateHasChanged();

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}