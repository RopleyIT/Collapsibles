using Microsoft.AspNetCore.Components;

namespace Tiling
{
    public partial class TileColumn
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
        /// the width of this column in its parent row, not its
        /// children which are arranged in a column. The same
        /// rules as flex-grow flex-shrink and flex-basis
        /// apply to the parent main axis. The cross axis is
        /// always set to align-items:stretch; so that everything
        /// is the same height (rows) or width (columns).
        /// </summary>
        [Parameter]
        public string Flex { get; set; } = "1 1 auto";

        /// <summary>
        /// Set the height of the flex column so that all children can be
        /// stretched to fit this height, rather than to sum of the
        /// heights of the children.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = string.Empty;

        /// <summary>
        /// Set the width of the flex column so that all children can be
        /// stretched to fit this width, rather than to the width of
        /// the widest element not hidden.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = string.Empty;

        private string DisplayStyle
        {
            get
            {
                string style = $"flex: {Flex};";
                if (!string.IsNullOrWhiteSpace(Width))
                    style += " width: " + Width + ";";
                if (!string.IsNullOrWhiteSpace(Height))
                    style += " height: " + Height + ";";
                return Visible ? style : "display: none;";
            }
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public List<ITileNode> Children { get; } = new();
    }
}