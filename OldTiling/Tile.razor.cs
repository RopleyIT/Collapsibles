using Microsoft.AspNetCore.Components;

namespace Tiling
{
    public partial class Tile
    {
        // Note the close.svg resource used on a button in the
        // markup above. Always put graphical resources like
        // this into the wwwroot folder of the component
        // library project, then reference it using the relative
        // URL "_content/{ProjectLibName}/resourceFileName"

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

        private string DisplayStyle => Visible ? $"flex: {Flex};" : "display: none;";

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

        [Parameter]
        public bool InitiallyHidden { get; set; } = false;

        [Parameter]
        public string Caption { get; set; } = string.Empty;

        private static List<ITileNode> children = new();
        public List<ITileNode> Children => children;
    }
}