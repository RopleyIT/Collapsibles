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

        private void ShowTile(Tile tile, bool showNotHide)
            => tile.Visible = showNotHide;

        /// <summary>
        /// Given the caption for a tile, make it visible or hidden.
        /// Requires an @ref for the TileSet in the page to access.
        /// </summary>
        /// <param name="caption">The title of the tile to be shown or hidden</param>
        /// <param name="showNotHide">True to show the tile, false to hide it</param>
        
        public void ShowTile(string caption, bool showNotHide)
        {
            Tile? tile = Children.FirstOrDefault(t => t.Caption == caption);
            if(tile != null)
                ShowTile(tile, showNotHide);
        }

        private void TileVisibilityChanged(Tile tile)
            => StateHasChanged();

        /// <summary>
        /// Set the height of the flex row so that all children can be
        /// stretched to fit this height, rather than to the height of
        /// the tallest element not hidden.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = string.Empty;

        /// <summary>
        /// Set the width of the flex row so that all children can be
        /// stretched to fit this height, rather than to the default
        /// value 'auto' that adds up the children's widths.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = string.Empty;

        private string DisplayStyle
        {
            get
            {
                string style = string.Empty;
                if (!string.IsNullOrWhiteSpace(Height))
                    style = "height: " + Height + ";";
                if (!string.IsNullOrWhiteSpace(Width))
                    style += "width: " + Width + ";";
                return style;
            }
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}