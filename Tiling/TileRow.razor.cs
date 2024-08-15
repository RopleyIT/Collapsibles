using Microsoft.AspNetCore.Components;

namespace Tiling
{
    public partial class TileRow
    {
        [CascadingParameter]
        private List<ITileNode>? Parent { get; set; } = null;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Parent?.Add(this);
        }

        public bool Visible => Children.Any(t => t.Visible);

        /// <summary>
        /// Main axis flex settings for this flex-item in its parent
        /// flex context. The following Flex attribute applies to
        /// the height of this row in its parent column, not its
        /// children which are arranged in a row. The same
        /// rules as flex-grow flex-shrink and flex-basis
        /// apply to the parent main axis. The cross axis is
        /// always set to align-items:stretch; so that everything
        /// is the same height (rows) or width (columns).
        /// </summary>
        [Parameter]
        public string Flex { get; set; } = "1 1 auto";

        private string DisplayStyle => Visible ? $"flex: {Flex};" : "display: none;";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public List<ITileNode> Children { get; } = new();
    }
}