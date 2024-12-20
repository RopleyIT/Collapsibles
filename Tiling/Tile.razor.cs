using Microsoft.AspNetCore.Components;

namespace Tiling
{
    public partial class Tile
    {
        [CascadingParameter]
        private TileSet? TileContainer { get; set; } = null;

        [CascadingParameter]
        private List<ITileNode>? Parent { get; set; } = null;

        /// <summary>
        /// Main axis flex settings for this flex-item. Has the
        /// same rules as flex-grow flex-shrink and flex-basis
        /// and only applies to the main axis. The cross axis is
        /// always set to align-items:stretch; so that everything
        /// is the same height (rows) or width (columns).
        /// </summary>
        [Parameter]
        public string Flex { get; set; } = "1 1 auto";

        private string DisplayStyle 
            => Visible && Selectable ? $"flex: {Flex};" : "display: none;";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TileContainer?.AddTile(this);
            Parent?.Add(this);
            Visible = !InitiallyHidden; // Set initial visibility
        }

        public EventCallback<Tile> VisibleChanged { get; set; }

        private bool visible = true;
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible != value)
                {
                    visible = value;
                    VisibleChanged.InvokeAsync(this);
                }
            }
        }

        /// <summary>
        /// Set to make the tile have a checkbox in the top bar
        ///  of the page. Clear to make the checkbox vanish.
        /// </summary>
        [Parameter]
        public bool Selectable { get; set; } = true;

        [Parameter]
        public bool InitiallyHidden { get; set; } = false;

        [Parameter]
        public string Caption { get; set; } = string.Empty;

        private static readonly List<ITileNode> children = [];
        public List<ITileNode> Children => children;
    }
}