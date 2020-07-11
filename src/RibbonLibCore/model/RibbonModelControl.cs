namespace RibbonLib.Model
{
    public class RibbonModelControl : RibbonModelItem
    {
        public object Content { get; set; }

        /// <inheritdoc />
        public override ControlKind Kind { get; } = ControlKind.Generic;
    }
}